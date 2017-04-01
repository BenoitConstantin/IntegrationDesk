using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using EquilibreGames;

public class CheckEventRealized : ConditionTask{

    public BBParameter<string> eventName;

    protected override bool OnCheck()
    {
        return PersistentDataSystem.Instance.GetSavedData<StoryTellingSavedData>().EventIsRealized(eventName.value);
    }
}
