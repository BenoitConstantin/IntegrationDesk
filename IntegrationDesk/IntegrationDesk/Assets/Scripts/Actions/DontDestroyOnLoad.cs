using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class DontDestroyOnLoad : ActionTask {

    public BBParameter<GameObject> gameObject;

    protected override void OnExecute()
    {
        GameObject.DontDestroyOnLoad(gameObject.value);
        EndAction();
    }
}
