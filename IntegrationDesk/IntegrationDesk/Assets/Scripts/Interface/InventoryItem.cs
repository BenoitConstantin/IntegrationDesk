using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Item pouvant être ajouté à l'inventaire
[System.Serializable]
public class InventoryItem {
    public string itemName;     // nom de l'objet
    public Sprite image;    // image de l'objet
    public StoryEvent obtainObjectEvent;    // événement à partir duquel le joueur à l'objet
}
