using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : IntegrationDeskObject {

    [SerializeField]
    string goToRoomName = "TedDesk";


    public void OnMouseDown()
    {
        ChangeRoom();
    }


    public void ChangeRoom()
    {
        GameManager.Instance.ChangeRoom(goToRoomName);
    }

}
