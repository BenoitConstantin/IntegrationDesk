using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Visuel d'un objet d'inventaire dans l'interface du joueur
public class InventoryItemGUI : MonoBehaviour {
    public Text itemName;           // label nom de l'item
    public Image itemImage;         // image de l'item
    public Button button;           // bouton correspondant à l'item
    InventoryItem inventoryItem;    // l'InventoryItem lié à cet InventoryItemGUI
    
    // active/désactive la sélection de l'item
    public void SetInteractable(bool interact)
    {
        button.interactable = interact;
    }
    
    // paramètre les composants du gameobject en fonction d'un InventoryItem
    public void FromInventoryItem(InventoryItem item) {
        inventoryItem = item;
        itemName.text = item.itemName;
        itemImage.sprite = item.image;
    }
    
    // appelé lors d'un clic sur l'objet
    public void OnClick()
    {
        HUD.Instance.OnInventoryItemClicked(inventoryItem);
    }
}
