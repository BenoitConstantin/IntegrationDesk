using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.StateMachines;

public class GameManager : Singleton<GameManager> {

    public FSMOwner stateMachine;
    public string currentDay = "Lundi";

    public bool ChangeRoom(string roomName)
    {
        return ((GameManagerState)(stateMachine.behaviour.currentState)).ChangeRoom(roomName);
    }

}
