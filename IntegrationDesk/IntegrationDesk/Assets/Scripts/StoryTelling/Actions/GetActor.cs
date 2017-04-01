using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using NodeCanvas.DialogueTrees;

public class GetActor : ActionTask {

    public string actorName;
    public BBParameter<DialogueActor> dialogueActor;
    public DialogueTreeController dialogueTreeController;

    protected override void OnExecute()
    {
        dialogueActor.value = StoryTelling.Instance.GetDialogueActor(actorName);
        dialogueTreeController.behaviour.actorParameters.Add(new DialogueTree.ActorParameter(actorName, dialogueActor.value));
        EndAction();
    }

}
