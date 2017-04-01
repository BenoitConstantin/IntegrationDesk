using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using NodeCanvas.DialogueTrees;

public class StoryTelling : Singleton<StoryTelling> {

    [System.Serializable]
    public struct ActorInfo
    {
        public DialogueActor dialogueActor;
    }

    public ActorInfo[] actorInfos;


    public DialogueActor GetDialogueActor(string actorName)
    {
        for (int i = 0; i < actorInfos.Length; i++)
        {
            if (actorInfos[i].dialogueActor.name.Equals(actorName))
                return actorInfos[i].dialogueActor;
        }
        return null;
    }

}
