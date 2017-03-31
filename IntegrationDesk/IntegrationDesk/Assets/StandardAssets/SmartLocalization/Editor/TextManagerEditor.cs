using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;
using SmartLocalization.Editor;
using SmartLocalization;

using EquilibreGames;

[CustomEditor(typeof(TextManager))]
public class TextManagerEditor : Editor {

    private List<string> selectedKeys = new List<string>();

     public override void OnInspectorGUI()
    {
       // base.OnInspectorGUI();
        TextManager textObject = ((TextManager)target);
        int cpt = 0;

        selectedKeys.Clear();


        if (textObject != null)
        {
            int value = (int)Mathf.Clamp(EditorGUILayout.IntField("SystemLanguage : " , textObject.systemLanguages.Count),0,Mathf.Infinity);

            if (value > textObject.systemLanguages.Count)
            {
                for (int i = value; textObject.systemLanguages.Count < value; i++)
                    textObject.systemLanguages.Add(new TextManager.LanguageConversionData());
            }
            else if (value < textObject.systemLanguages.Count)
            {
                for (int i = value; textObject.systemLanguages.Count > value; i--)
                    textObject.systemLanguages.RemoveAt(textObject.systemLanguages.Count - 1);
            }


            foreach (TextManager.LanguageConversionData i in textObject.systemLanguages)
            {
                i.systemLanguage = (SystemLanguage)EditorGUILayout.EnumPopup("SystemLanguage : ", i.systemLanguage);
                i.smartLocalizationKey = EditorGUILayout.TextField("SmartLocalisaiton Key : ", i.smartLocalizationKey);
                GUILayout.Space(5);
            }


            GUILayout.Space(20);



            value = (int)Mathf.Clamp(EditorGUILayout.IntField("SmartText : ", textObject.smartTextData.Count), 0, Mathf.Infinity);

            if (value > textObject.smartTextData.Count)
            {
                for (int i = value; textObject.smartTextData.Count < value; i++)
                    textObject.smartTextData.Add(new TextManager.SmartTextData());
            }
            else if (value < textObject.smartTextData.Count)
            {
                for (int i = value; textObject.smartTextData.Count > value; i--)
                textObject.smartTextData.RemoveAt(textObject.smartTextData.Count - 1);
            }


            foreach (TextManager.SmartTextData i in textObject.smartTextData)
            {
                selectedKeys.Add(i.localizedKey);
            }

            foreach (TextManager.SmartTextData i in textObject.smartTextData)
            {
                i.text = (Text)EditorGUILayout.ObjectField(i.text, typeof(Text), true);
                i.localizedKey = EditorGUILayout.TextField(i.localizedKey);

                selectedKeys[cpt] = LocalizedKeySelector.SelectKeyGUI(selectedKeys[cpt], true, LocalizedObjectType.STRING);

                if (!Application.isPlaying && GUILayout.Button("Use Key", GUILayout.Width(70)))
                {
                    i.localizedKey = selectedKeys[cpt];
                }
                GUILayout.Space(10);
                cpt++;
            }
        }
    }
}