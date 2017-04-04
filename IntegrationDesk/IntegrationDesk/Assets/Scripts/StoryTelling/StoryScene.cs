using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.BehaviourTrees;
using System;
using NodeCanvas.DialogueTrees;

public class StoryScene : MonoBehaviour {

    public static Action<string> OnSpeakWithActor;

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


    public void SpeakWithActor(string actorName)
    {
        behaviourTree.SendEvent<string>("Speak", actorName);
    }
}
