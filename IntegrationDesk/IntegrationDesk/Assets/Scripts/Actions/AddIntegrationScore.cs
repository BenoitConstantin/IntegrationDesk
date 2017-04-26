using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using EquilibreGames;

public class AddIntegrationScore : ActionTask {

    public int amount = 5;

    protected override void OnExecute()
    {
        base.OnExecute();
        PersistentDataSystem.Instance.GetSavedData<PlayerSavedData>().integrationScore += amount;
        EndAction();
    }
}
