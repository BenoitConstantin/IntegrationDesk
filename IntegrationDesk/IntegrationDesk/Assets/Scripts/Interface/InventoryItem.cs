using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Item pouvant être ajouté à l'inventaire
[System.Serializable]
public class InventoryItem {
    public string name;     // nom de l'objet
    public Sprite image;    // image de l'objet
    
    
    public static bool operator ==(InventoryItem item1, InventoryItem item2){
        return item1.name == item2.name;
    }

    public static bool operator !=(InventoryItem item1, InventoryItem item2){
        return item1.name != item2.name;
    }
    public override bool Equals(object obj){
        if (ReferenceEquals(null, obj))
        {
            return false;
        }
        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        return obj.GetType() == GetType() && name == (obj as InventoryItem).name;
    }
    public override int GetHashCode()
    {
        unchecked
        {
            return name.GetHashCode();
        }
    }
}
