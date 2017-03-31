using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using SmartLocalization.Editor;
using SmartLocalization;


namespace EquilibreGames
{
    public class TextManager : Singleton<TextManager>
    {
        public enum TEXTMANAGEREVENT { ONLANGUAGECHANGED, }

        [System.Serializable]
        public class SmartTextData
        {
            public Text text;
            public string localizedKey = "INSERT_KEY_HERE";
        }

        public List<SmartTextData> smartTextData = new List<SmartTextData>();

        [System.Serializable]
        public class LanguageConversionData
        {
            public SystemLanguage systemLanguage;
            public string smartLocalizationKey;
        }


        public string currentLanguage;

        public List<LanguageConversionData> systemLanguages;


        void Awake()
        {
            SmartLocalization.LanguageManager languageManager = SmartLocalization.LanguageManager.Instance;

            //Subscribe to the change language event
            languageManager.OnChangeLanguage += OnChangeLanguage;

            foreach (LanguageConversionData i in systemLanguages)
            {
                if (Application.systemLanguage == i.systemLanguage)
                {
                    SmartLocalization.LanguageManager.Instance.ChangeLanguage(i.smartLocalizationKey);
                    return;
                }
            }



            //Run the method one first time if no language correspondance found.
            ChangeLanguage(SmartLocalization.LanguageManager.Instance.defaultLanguage);
        }


        void OnDestroy()
        {
            if (SmartLocalization.LanguageManager.HasInstance)
            {
                SmartLocalization.LanguageManager.Instance.OnChangeLanguage -= OnChangeLanguage;
            }
        }


        public void ChangeLanguage(SystemLanguage systemLanguage)
        {
            foreach (LanguageConversionData i in systemLanguages)
            {
                if (Application.systemLanguage == i.systemLanguage)
                {
                    SmartLocalization.LanguageManager.Instance.ChangeLanguage(i.smartLocalizationKey);
                    currentLanguage = i.smartLocalizationKey;
                    return;
                }
            }
        }

        public void ChangeLanguage(string smartLocalizationKeyLanguage)
        {
            SmartLocalization.LanguageManager.Instance.ChangeLanguage(smartLocalizationKeyLanguage);
            currentLanguage = smartLocalizationKeyLanguage;
        }


        void OnChangeLanguage(LanguageManager languageManager)
        {
#if EQUILIBRE_GAMES_DEBUG
            Debug.Log("Language changed");
#endif
            foreach (SmartTextData i in smartTextData)
            {
                i.text.text = languageManager.GetTextValue(i.localizedKey);
            }
#if EVENTHANDLER
            EventHandler.TriggerEvent(TEXTMANAGEREVENT.ONLANGUAGECHANGED);
#endif
        }

    }
}
