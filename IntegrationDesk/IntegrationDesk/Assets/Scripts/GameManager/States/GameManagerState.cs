using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.StateMachines;
using NodeCanvas.Framework;
using UnityEngine.SceneManagement;

public abstract class GameManagerState : ActionState {

    public BBParameter<GameManager> gameManager;


    public bool ChangeRoom(string roomName)
    {
        FSM.SendEvent(roomName);
        return true;
    }

}
