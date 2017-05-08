using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntegrationDeskQuizz : MonoBehaviour {
    public List<QuizzQuestion> questions;   // les questions du quizz
    public StoryEvent onSuccessUnlockEvent; // le storyevent à unlocker lorsqu'on réussi le test
    public float successRatioThreshold; // à partir de quel ration (bonne réponses/mauvaise) réussi-t'on le test (recommandé: 0.9)
}
