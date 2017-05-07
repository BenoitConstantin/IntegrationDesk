using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EquilibreGames;

public class Notebook : MonoBehaviour {
    static Notebook instance;
    public static Notebook Instance {
        get { return instance; }
    }
    
    [System.Serializable]
    public class Entry {
        public string content;
        public StoryEvent unlockEvent;
    }
    
    public Text textObj;            // l'objet dans lequel est affiché le contenu du journal
    public List<Entry> entries;     // les entrées du journal. Nécessite d'appeler UpdateNotebookContent après mise à jour
    public CanvasGroup canvasGroup; // le CanvasGroup gérant le notebook (permet de masquer celui-ci)
    bool visible = true;
    
	void Start () {
        if (instance == null)
            instance = this;
        UpdateNotebookContent();
        Hide();
	}
    
    // met à jour le texte du notebook
    public void UpdateNotebookContent()
    {
        string combined = "";
        foreach (Entry entry in entries)
            if (PersistentDataSystem.Instance.GetSavedData<StoryTellingSavedData>().EventIsRealized(entry.unlockEvent))
                combined += entry.content + "\n\n";
        textObj.text = combined;
    }
    
    
    // masque l'intégralité de l'interface
    public void Hide()
    {
        visible = canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }
    // affiche l'intégralité de l'interface
    public void Show()
    {
        visible = canvasGroup.interactable = canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        UpdateNotebookContent();
    }
    // retourne vrai si l'interface est affichée
    public bool IsVisible()
    {
        return visible;
    }
}
