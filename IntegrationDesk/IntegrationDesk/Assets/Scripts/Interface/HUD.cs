using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EquilibreGames;

// Script gérant l'interface du joueur
public class HUD : MonoBehaviour {
    static HUD instance;
    public static HUD Instance {
        get {
            return instance;
        }
    }
    
    public CanvasGroup canvasGroup;             // le CanvasGroup gérant l'interface (permet de masquer celle-ci)
    public CanvasGroup actionBarCanvasGroup;    // le CanvasGroup gérant la barre d'action
    public CanvasGroup notebookCanvasGroup;     // le CanvasGroup gérant le bouton du notebook
    // [HideInInspector]
    public List<InventoryItem> inventoryItems;  // la liste des items dans l'inventaire (nécessite d'appeler RefreshInventoryView après modif)
    public GameObject inventoryItemGUI_Prefab;  // prefab d'un item visible dans l'inventaire
    public RectTransform inventoryPanel;        // panneau dans lequel sont ajoutés les items d'inventaire
    
    bool visible = true;
    List<InventoryItemGUI> inventoryItemGUIs;   // liste des items dans l'inventaire (gameobjects, pas classes)
    
    public enum HUDMode {                       // type d'interaction possibles dans le hud (pour les 3 boutons de gauche)
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
            buttons.LookButton.interactable = isNPC && isActive;
            buttons.ShowButton.interactable = isNPC && isActive;
            foreach (InventoryItemGUI item in inventoryItemGUIs)
                item.SetInteractable(isNPCShow);
            actionBarCanvasGroup.alpha = isNPC ? 1: .75f;
            actionBarCanvasGroup.interactable = actionBarCanvasGroup.blocksRaycasts = isNPC;
        }
        get {
            return mode;
        }
    }
    bool hasNotebook;
    public bool HasNotebook {
        get { return hasNotebook; }
        set {
            hasNotebook = value;
            notebookCanvasGroup.alpha = value ? 1:0;
            notebookCanvasGroup.interactable = notebookCanvasGroup.blocksRaycasts = value;
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
    
    public IntegrationDeskNPC currentNPC;       // le NPC actuellement affiché (on peut lui parler ou lui montrer un objet)
    public Image npcBackground;                 // l'image de fond lors de dialogue avec un NPC
    public Image npcSprite;                     // le sprite du NPC
    
    
    
    void Start()
    {
        if (instance == null)
            instance = this;
        Show();
        inventoryItemGUIs = new List<InventoryItemGUI>();
        RefreshInventoryView();
        Quizz.Instance.Hide();
        Notebook.Instance.Hide();
        Mode = HUDMode.none;
        DeselectNPC();
        RefreshFromSavedSettings();
        StartCoroutine(RefreshInventoryViewCoroutineHack());
    }
    
    // Restaure/rafraichit la config de l'UI en fonction des données de jeu sauvegardées (inventaire, intégration, etc.)
    public void RefreshFromSavedSettings()
    {
        progressPanel.Day = GameManager.Instance.currentDay;
        PlayerSavedData data = PersistentDataSystem.Instance.GetSavedData<PlayerSavedData>();
        progressPanel.Progression = data.integrationScore / 100;
        // inventoryItems = data.inventoryItems;
        HasNotebook = data.hasNotebook;
        RefreshInventoryView();
    }
    
    // Supprime et réinstancie les visuels de l'inventaire
    public void RefreshInventoryView() {
        int numItems = 0;
        foreach (InventoryItem item in inventoryItems)
            if (PersistentDataSystem.Instance.GetSavedData<StoryTellingSavedData>().EventIsRealized(item.obtainObjectEvent))
                numItems++;
        if (numItems == inventoryItemGUIs.Count)
            return;
        
        foreach (InventoryItemGUI gui in inventoryItemGUIs)
            Destroy(gui.gameObject);
        inventoryItemGUIs.Clear();
        foreach (InventoryItem item in inventoryItems)
        {
            if (!PersistentDataSystem.Instance.GetSavedData<StoryTellingSavedData>().EventIsRealized(item.obtainObjectEvent))
                continue;
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
        
        if (currentNPC == null)
        {
            Debug.LogError("HUD: tentative de parler à un NPC mais aucun NPC n'a été sélectionné ?");
            return;
        }
        currentNPC.Speak();
    }
    // Appelé lors d'un clic sur le bouton "chercher"
    public void OnLookButton()
    {
        Debug.Log("Action CHERCHER");
        
        DeselectNPC();
    }
    // Appelé lors d'un clic sur le bouton "montrer"
    public void OnShowButton()
    {
        Debug.Log("Action MONTRER");
        Mode = HUDMode.npcShow;
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
        Debug.Log("Clic objet " + item.itemName);
        if (currentNPC != null)
            currentNPC.Show(item.itemName);
        else
            Debug.LogError("Impossible de montrer l'objet car pas de NPC");
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
    
    public void SelectNPC(IntegrationDeskNPC npc)
    {
        currentNPC = npc;
        Mode = HUDMode.npc;
        // npcSprite.sprite = npc.NpcSprite;
        // npcSprite.color = Color.white;
        // npcBackground.color = Color.white;
    }
    public void DeselectNPC()
    {
        currentNPC = null;
        Mode = HUDMode.none;
        npcSprite.sprite = null;
        npcSprite.color = new Color(0,0,0,0);
        npcBackground.color = new Color(0,0,0,0);
    }
    
    IEnumerator RefreshInventoryViewCoroutineHack()
    {
        while (true)
        {
            RefreshInventoryView();
            yield return new WaitForSeconds(1);
        }
    }
}
