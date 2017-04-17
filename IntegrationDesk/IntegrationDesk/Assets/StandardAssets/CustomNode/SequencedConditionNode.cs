using UnityEngine;
using System.Collections.Generic;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using NodeCanvas.DialogueTrees;
using NodeCanvas;

#if UNITY_EDITOR
using UnityEditor;
#endif


[Name("Sequenced Task Condition")]
[Category("Flow Control")]
[Description("Will continue the sequence of dialogue, incrementing the counter bye one each time")]
public class SequencedConditionNode : DTNode
{
    public enum MODE { MOD, LIMIT_TO_MAX, STOP_TO_MAX}

    public MODE mode;
    private int count = -1;

    public override int maxOutConnections
    {
        get { return -1; }
    }



    protected override Status OnExecute(Component agent, IBlackboard bb)
    {
        if (outConnections.Count == 0)
            return Error("There are no connections on the Dialogue Condition Node");

            switch (mode)
            {
                case MODE.MOD:  count = (count+1)%maxOutConnections; break;
                case MODE.LIMIT_TO_MAX: count++; if (count > outConnections.Count - 1) count = outConnections.Count - 1; break;
            }

        if (count < outConnections.Count)
        {
            DLGTree.Continue(count);
            return Status.Success;
        }
        else
        {
            DLGTree.Stop(false);
            return Status.Failure;
        }
    }


    ////////////////////////////////////////
    ///////////GUI AND EDITOR STUFF/////////
    ////////////////////////////////////////
#if UNITY_EDITOR
    protected override void OnNodeGUI()
    {
        mode = (MODE)EditorGUILayout.EnumPopup(mode);
    }
#endif
}