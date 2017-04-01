using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using NodeCanvas.DialogueTrees;

public class CheckDialogueRunning : ConditionTask {

    protected override bool OnCheck()
    {
        return DialogueTree.currentDialogue && DialogueTree.currentDialogue.isRunning;
    }
}
