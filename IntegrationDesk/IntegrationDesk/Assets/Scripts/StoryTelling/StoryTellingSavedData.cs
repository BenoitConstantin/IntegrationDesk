using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EquilibreGames;

[System.Serializable]
public class StoryTellingSavedData : SavedData {


    List<string> eventRealized = new List<string>(); 


    public bool EventIsRealized(string eventName)
    {
        return eventRealized.Find( (x =>  x.Equals(eventName) )) != null;
    }

    public void RealizeEvent(string eventName)
    {
        if(!EventIsRealized(eventName))
        {
            eventRealized.Add(eventName);
        }
    }
}
