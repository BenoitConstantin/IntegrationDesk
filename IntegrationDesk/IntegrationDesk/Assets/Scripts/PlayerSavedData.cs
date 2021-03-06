﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EquilibreGames;

[System.Serializable]
public class PlayerSavedData : SavedData {

    public int integrationScore = 0;
    public List<InventoryItem> inventoryItems = new List<InventoryItem>();
    public bool hasNotebook = false;

}
