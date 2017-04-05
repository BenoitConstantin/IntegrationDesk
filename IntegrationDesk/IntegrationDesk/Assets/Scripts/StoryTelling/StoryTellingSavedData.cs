using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EquilibreGames;

[System.Serializable]
public class StoryTellingSavedData : SavedData {


    public List<StoryEvent> eventRealized = new List<StoryEvent>(); 


    public bool EventIsRealized(StoryEvent storyEvent)
    {
       foreach(StoryEvent i in eventRealized)
        {
            if (i == storyEvent)
                return true;
        }

        return false;
    }

    public void RealizeEvent(StoryEvent storyEvent)
    {
        if(!EventIsRealized(storyEvent))
        {
            eventRealized.Add(storyEvent);
        }
    }
}
