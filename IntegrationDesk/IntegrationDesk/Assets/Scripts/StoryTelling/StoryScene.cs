using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.BehaviourTrees;
using System;
using NodeCanvas.DialogueTrees;

public class StoryScene : MonoBehaviour {

    public static Func<string, bool> OnSpeakWithActor;

    [SerializeField]
    BehaviourTreeOwner behaviourTree;

    [SerializeField]
    DialogueTreeController dialogueTreeController;

    void Awake()
    {
        OnSpeakWithActor += SpeakWithActor;
    }

    void OnDestroy()
    {
        OnSpeakWithActor -= SpeakWithActor;
    }


    public bool SpeakWithActor(string actorName)
    {
        behaviourTree.SendEvent("Speak", actorName);
        return true;
    }

    public bool UseObject(IntegrationDeskObject integrationDeskObject)
    {
        behaviourTree.SendEvent("Object", integrationDeskObject.StoryEvent);
        dialogueTreeController.blackboard.SetValue("object", integrationDeskObject);
        return true;
    }
}
