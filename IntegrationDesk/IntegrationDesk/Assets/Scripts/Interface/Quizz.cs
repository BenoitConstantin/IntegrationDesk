using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script gérant l'interface de quizz
public class Quizz : MonoBehaviour {
    static Quizz instance;
    public static Quizz Instance {
        get { return instance; }
    }
    
    public RectTransform contentPanel;      // le panneau dans lequel on ajoute les visuels des questions-réponses (QR)
    public GameObject questionPrefab;       // le prefab de QR qu'on instancie dans le contentPanel
    List<QuizzQuestionGUI> questionGUIs;    // les prefab de QR actuellement instanciés
    
    // [HideInInspector]
    public List<QuizzQuestion> questions;   // les questions du quizz
    public CanvasGroup canvasGroup;         // le CanvasGroup gérant le quizz (permet de masquer celui-ci)
    bool visible = true;
    
    void Start() {
        if (instance == null)
            instance = this;
        questionGUIs = new List<QuizzQuestionGUI>();
        RefreshQuestionsGUI();
    }
    
    // Supprime et réinstancie les visuels des QR
    public void RefreshQuestionsGUI() {
        foreach (QuizzQuestionGUI child in questionGUIs)
            Destroy(child);
        
        foreach (QuizzQuestion question in questions)
        {
            GameObject questionGUI_GO = Instantiate(questionPrefab);
            questionGUI_GO.transform.SetParent(contentPanel, false);
            QuizzQuestionGUI questionGUI = questionGUI_GO.GetComponent<QuizzQuestionGUI>();
            questionGUI.FromQuizzQuestion(question);
            questionGUIs.Add(questionGUI);
        }
    }

    // Appelé lorsque le joueur valide le questionnaire
    public void OnSubmitBtn() {
        Debug.Log("Quizz : bouton terminé");
        
        int score = 0;
        foreach (QuizzQuestionGUI questionGUI in questionGUIs)
            if (questionGUI.IsRight())
                score++;
        
        Debug.Log("Score : " + score + "/" + questionGUIs.Count);
        
        // TODO
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
