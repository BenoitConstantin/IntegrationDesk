using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryActor : MonoBehaviour {

    public string actorName;


    void OnMouseDown()
    {
        if(StoryScene.OnSpeakWithActor != null)
            StoryScene.OnSpeakWithActor(actorName);
    }
}
