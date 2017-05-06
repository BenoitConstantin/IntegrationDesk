using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using EquilibreGames;


/// Permet d'activer (ou de désactiver) l'icône notebook du joueur
public class EnableNotebook : ActionTask {

    public bool enable = true;

    protected override void OnExecute()
    {
        base.OnExecute();
        PersistentDataSystem.Instance.GetSavedData<PlayerSavedData>().hasNotebook = enable;
        HUD.Instance.RefreshFromSavedSettings();
        EndAction();
    }
}
