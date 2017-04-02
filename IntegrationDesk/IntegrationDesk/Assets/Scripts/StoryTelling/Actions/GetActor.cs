using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using NodeCanvas.DialogueTrees;

public class GetActor : ActionTask {

    public string actorName;
    public DialogueTreeController dialogueTreeController;

    protected override void OnExecute()
    {
        dialogueTreeController.behaviour.SetActorReference(actorName, StoryTelling.Instance.GetDialogueActor(actorName));
        EndAction();
    }

}
