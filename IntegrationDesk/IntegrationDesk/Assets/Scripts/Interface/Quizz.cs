using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EquilibreGames;

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
    public IntegrationDeskQuizz idquizz;
    public CanvasGroup canvasGroup;         // le CanvasGroup gérant le quizz (permet de masquer celui-ci)
    public CanvasGroup scorePopupCanvasGroup;
    public CanvasGroup DawnOf;
    public Text scorePopupScore;
    bool visible = true;
    bool finished = false;
    public bool IsFinished {                // retourne vrai si le joueur a terminé de remplir le questionnaire
        get {
            return finished;
        }
    }
    
    
    void Start() {
        if (instance == null)
            instance = this;
        questionGUIs = new List<QuizzQuestionGUI>();
        RefreshQuestionsGUI();
    }
    
    // Supprime et réinstancie les visuels des QR
    public void RefreshQuestionsGUI() {
        foreach (QuizzQuestionGUI child in questionGUIs)
            Destroy(child.gameObject);
        questionGUIs.Clear();
        
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
        string str = "";
        
        foreach (QuizzQuestionGUI questionGUI in questionGUIs)
        {
            str += questionGUI.question.question + "\n";
            if (questionGUI.IsRight())
            {
                PersistentDataSystem.Instance.GetSavedData<PlayerSavedData>().integrationScore += (int)questionGUI.question.addToIntegration;
                str += "    Vous avez bien répondu !\n";
                score++;
            }
            else
                str += "    Vous n'avez pas donné la bonne réponse !\n";
            str += "    La réponse est :\n";
            foreach (QuizzQuestion.QuizzAnswer answ in questionGUI.question.answers)
                if (answ.isRight)
                    str += "       " + answ.name + "\n";
            str += questionGUI.question.explanation;
            str += "\n";
            str += "\n";
        }
        
        Debug.Log("Score : " + score + "/" + questionGUIs.Count);
        scorePopupScore.text = "Score : " + score + "/" + questionGUIs.Count + "\n\n" + str;
        
        float successRatio = score / (float)questionGUIs.Count;
        if (successRatio >= idquizz.successRatioThreshold)
            PersistentDataSystem.Instance.GetSavedData<StoryTellingSavedData>().RealizeEvent(idquizz.onSuccessUnlockEvent);
        
        scorePopupCanvasGroup.interactable = scorePopupCanvasGroup.blocksRaycasts = true;
        scorePopupCanvasGroup.alpha = 1;
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
        scorePopupCanvasGroup.interactable = scorePopupCanvasGroup.blocksRaycasts = false;
        DawnOf.alpha = 0;
        canvasGroup.alpha = 1;
        scorePopupCanvasGroup.alpha = 0;
        finished = false;
    }
    public void ShowEEgg()
    {
        canvasGroup.alpha = 1;
        DawnOf.alpha = 1;
    }
    // retourne vrai si l'interface est affichée
    public bool IsVisible()
    {
        return visible;
    }
    
    public void OnReturnToGameButton()
    {
        HUD.Instance.RefreshFromSavedSettings();
        finished = true;
        Hide();
    }
}
