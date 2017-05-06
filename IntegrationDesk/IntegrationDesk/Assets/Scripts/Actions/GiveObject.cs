using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using EquilibreGames;


/// Permet d'ajouter un objet à l'inventaire du joueur
public class GiveObject : ActionTask {

    public InventoryItem item;

    protected override void OnExecute()
    {
        base.OnExecute();
        List<InventoryItem> items = PersistentDataSystem.Instance.GetSavedData<PlayerSavedData>().inventoryItems;
        Debug.Log("Ajout de l'item " + item.name);
        if (!items.Contains(item))
            items.Add(item);
        HUD.Instance.RefreshFromSavedSettings();
        EndAction();
    }
}
