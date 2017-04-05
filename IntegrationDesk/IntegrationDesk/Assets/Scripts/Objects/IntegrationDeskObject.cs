using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntegrationDeskObject : MonoBehaviour {

    [SerializeField]
    StoryScene storyScene;

    [SerializeField]
    StoryEvent storyEvent;

    public StoryEvent StoryEvent
    {
        get { return storyEvent; }
    }


    public void OnMouseDown()
    {
        storyScene.UseObject(this);
    }



}
