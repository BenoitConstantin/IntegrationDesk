using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Question pouvant être posée lors d'un quizz, et ses réponses
[System.Serializable]
public class QuizzQuestion {
    [System.Serializable]
    public class QuizzAnswer {  // une des réponses possibles à la question
        public string name;     // le nom (=texte) de la réponse
        public bool isRight;    // vrai si la réponse est la bonne
    }
    
    public string question;     // intitulé de la question
    public QuizzAnswer[] answers; // les réponses proposées /!\ Pas plus de 4 réponses
}
