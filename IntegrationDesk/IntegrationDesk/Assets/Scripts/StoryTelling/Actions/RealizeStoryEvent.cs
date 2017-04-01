using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using EquilibreGames;

public class RealizeStoryEvent : ActionTask {

    public BBParameter<string> eventName;

    protected override void OnExecute()
    {
        PersistentDataSystem.Instance.GetSavedData<StoryTellingSavedData>().RealizeEvent(eventName.value);
        EndAction();
    }
}
