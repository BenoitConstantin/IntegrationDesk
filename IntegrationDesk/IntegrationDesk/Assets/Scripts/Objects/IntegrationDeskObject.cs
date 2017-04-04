using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntegrationDeskObject : MonoBehaviour {

    [SerializeField]
    StoryScene storyScene;

    [SerializeField]
    string objectName;

    public string ObjectName
    {
        get { return objectName; }
    }


    public void OnMouseDown()
    {
        storyScene.UseObject(this);
    }



}
