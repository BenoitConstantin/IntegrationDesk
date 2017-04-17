using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Script gérant l'interface du joueur
public class HUD : MonoBehaviour {
    static HUD instance;
    public static HUD Instance {
        get {
            return instance;
        }
    }
    
    public CanvasGroup canvasGroup;             // le CanvasGroup gérant l'interface (permet de masquer celle-ci)
    // [HideInInspector]
    public List<InventoryItem> inventoryItems;  // la liste des items dans l'inventaire (nécessite d'appeler RefreshInventoryView après modif)
    public GameObject inventoryItemGUI_Prefab;  // prefab d'un item visible dans l'inventaire
    public RectTransform inventoryPanel;        // panneau dans lequel sont ajoutés les items d'inventaire
    
    bool visible = true;
    List<InventoryItemGUI> inventoryItemGUIs;   // liste des items dans l'inventaire (gameobjects, pas classes)
    
    public enum HUDMode {                       // type d'interaction possibles dans le hud (pour les 3 boutons de gauche)
        exploration,                            // uniquement "chercher" est disponible
        npc,                                    // uniquement "montrer" et "parler" sont dispo
        npcShow,                                // uniquement "montrer" et "parler" sont dispo, la barre d'inventaire est disponible
        none                                    // rien n'est dispo
    }
    HUDMode mode;
    public HUDMode Mode {                       // permet de changer les 3 boutons de gauche disponibles dans l'interface
        set {
            mode = value;
            bool isNPC = mode == HUDMode.npc || mode == HUDMode.npcShow;
            bool isNPCShow = mode == HUDMode.npcShow;
            bool isActive = mode != HUDMode.none;
            buttons.SpeakButton.interactable = isNPC && isActive;
            buttons.LookButton.interactable = !isNPC && isActive;
            buttons.ShowButton.interactable = isNPC && isActive;
            foreach (InventoryItemGUI item in inventoryItemGUIs)
                item.SetInteractable(isNPCShow);
        }
        get {
            return mode;
        }
    }
    
    [System.Serializable]
    public class ProgressPanel {                // informations de progression de l'intégration
        public Text dayLabel;                   // texte contenant le nom du jour
        public Image progressionImage;          // image de la barre de progression
        
        string day;
        public string Day {                     // nom du jour actuel
            get { return day; }
            set {
                day = value;
                if (dayLabel != null)
                    dayLabel.text = day;
            }
        }
        float progression;
        public float Progression {              // pourcentage d'intégration
            get { return progression; }
            set {
                progression = Mathf.Clamp(value, 0, 1);
                if (progressionImage != null)
                    progressionImage.fillAmount = progression;
            }
        }
        public ProgressPanel() {
            Day = "Lundi"; // yé hais lé lundi
            Progression = .33f;
        }
    }
    public ProgressPanel progressPanel;         // panneau contenant les informations de journée et d'intégration
    
    [System.Serializable]
    public class Buttons {                      // panneau contenant les boutons d'action
        public Button SpeakButton;
        public Button LookButton;
        public Button ShowButton;
        public Button NotebookButton;
    }
    public Buttons buttons;                     // groupe de boutons d'action
    
    
    
    void Start()
    {
        if (instance == null)
            instance = this;
        inventoryItemGUIs = new List<InventoryItemGUI>();
        RefreshInventoryView();
        Quizz.Instance.Hide();
        Notebook.Instance.Hide();
        Mode = HUDMode.none;
    }
    
    // Supprime et réinstancie les visuels de l'inventaire
    public void RefreshInventoryView() {
        
        foreach (InventoryItemGUI gui in inventoryItemGUIs)
            Destroy(gui);
        foreach (InventoryItem item in inventoryItems)
        {
            GameObject itemGuiGO = Instantiate(inventoryItemGUI_Prefab);
            InventoryItemGUI itemGui = itemGuiGO.GetComponent<InventoryItemGUI>();
            itemGui.FromInventoryItem(item);
            itemGuiGO.transform.SetParent(inventoryPanel, false);
            inventoryItemGUIs.Add(itemGui);
        }
    }
    
    // Appelé lors d'un clic sur le bouton "parler"
    public void OnSpeakButton()
    {
        Debug.Log("Action PARLER");
        if (Mode == HUDMode.npcShow)
            Mode = HUDMode.npc;
        // TODO
    }
    // Appelé lors d'un clic sur le bouton "chercher"
    public void OnLookButton()
    {
        Debug.Log("Action CHERCHER");
        // TODO
    }
    // Appelé lors d'un clic sur le bouton "montrer"
    public void OnShowButton()
    {
        Debug.Log("Action MONTRER");
        Mode = HUDMode.npcShow;
        // TODO
    }
    // Appelé lors d'un clic sur le bouton notebook
    public void OnOpenNotebook()
    {
        Debug.Log("Action NOTEBOOK");
        Notebook.Instance.Show();
    }
    
    // Appelé lors d'un clic sur un des boutons d'inventaires
    public void OnInventoryItemClicked(InventoryItem item)
    {
        Debug.Log("Clic objet " + item.name);
        // TODO
    }
    
    // masque l'intégralité de l'interface
    public void Hide()
    {
        visible = canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }
    // affiche l'intégralité de l'interface
    public void Show()
    {
        visible = canvasGroup.interactable = canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }
    // retourne vrai si l'interface est affichée
    public bool IsVisible()
    {
        return visible;
    }
}
