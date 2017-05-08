using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.BehaviourTrees;
using NodeCanvas.DialogueTrees;

public class KickstartMyQuizz : MonoBehaviour {

    [SerializeField]
    BehaviourTreeOwner behaviourTree;

    [SerializeField]
    DialogueTreeController dialogueTreeController;

	void Start () {
		StartCoroutine(kickstartQuizz());
	}
    IEnumerator kickstartQuizz()
    {
        yield return new WaitForSeconds(1); // tempo au cas où (sais pas si on peut envoyer des events direct au behaviortree ?)
        behaviourTree.SendEvent("Quizz", "Kickstart"); // kickstart my heart ! whooo-hooo...
    }
}
