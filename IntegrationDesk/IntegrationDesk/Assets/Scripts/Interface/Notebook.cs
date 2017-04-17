using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notebook : MonoBehaviour {
    static Notebook instance;
    public static Notebook Instance {
        get { return instance; }
    }
    
    public Text textObj;            // l'objet dans lequel est affiché le contenu du journal
    public List<string> entries;    // les entrées du journal. Nécessite d'appeler UpdateNotebookContent après mise à jour
    public CanvasGroup canvasGroup; // le CanvasGroup gérant le notebook (permet de masquer celui-ci)
    bool visible = true;
    
	void Start () {
        if (instance == null)
            instance = this;
        UpdateNotebookContent();
	}
    
    // met à jour le texte du notebook
    public void UpdateNotebookContent()
    {
        string combined = "";
        foreach (string entry in entries)
            combined += entry + "\n\n";
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
    }
    // retourne vrai si l'interface est affichée
    public bool IsVisible()
    {
        return visible;
    }
}
