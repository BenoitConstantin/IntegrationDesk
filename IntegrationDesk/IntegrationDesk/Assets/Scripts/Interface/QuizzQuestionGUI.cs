using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Visuel de question-réponse (QR) de quizz
public class QuizzQuestionGUI : MonoBehaviour {
    public Text questionLabel;          // label énoncant la question
    public GameObject[] answerGOs;      // gameobjects des checkboxes réponses
    public Text[] answerLabels;         // label des checkboxes réponses
    public Toggle[] answerToggles;      // toggle des checkboxes réponses
    QuizzQuestion question;             // question liée à ce visuel
    
    // paramètre les différents champs en fonction d'une QuizzQuestion
    public void FromQuizzQuestion(QuizzQuestion question) {
        this.question = question;
        questionLabel.text = question.question;
        for (int i=0; i<4; i++)
        {
            if (question.answers.Length > i)
            {
                answerGOs[i].SetActive(true);
                answerLabels[i].text = question.answers[i].name;
            }
            else
                answerGOs[i].SetActive(false);
        }
    }
    
    // retourne vrai si la question a bien été répondue
    // /!\ si plusieurs réponses sont marquées comme étant les bonnes, il faudra que le
    // joueur coche EXACTEMENT l'intégralité des bonnes réponses.
    // Si besoin d'un autre comportement, ne pas hésiter à modifier ce code
    public bool IsRight() {
        for (int i=0; i<question.answers.Length; i++)
        {
            if (answerToggles[i].isOn != question.answers[i].isRight)
                return false;
        }
        return true;
    }
}
