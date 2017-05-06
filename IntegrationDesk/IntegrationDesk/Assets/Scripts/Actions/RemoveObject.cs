using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using EquilibreGames;


/// Permet d'enlever un objet de l'inventaire du joueur
public class RemoveObject : ActionTask {

    public InventoryItem item;

    protected override void OnExecute()
    {
        base.OnExecute();
        List<InventoryItem> items = PersistentDataSystem.Instance.GetSavedData<PlayerSavedData>().inventoryItems;
        Debug.Log("Suppression de l'item " + item.name);
        if (items.Contains(item))
            items.Remove(item);
        HUD.Instance.RefreshFromSavedSettings();
        EndAction();
    }
}
