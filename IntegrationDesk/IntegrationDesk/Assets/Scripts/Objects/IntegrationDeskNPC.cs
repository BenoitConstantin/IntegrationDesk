using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Similaire à IntegrationDeskObject, mais avec sélection du NPC et action via interface
public class IntegrationDeskNPC : MonoBehaviour {

    [SerializeField]
    StoryScene storyScene;

    [SerializeField]
    StoryEvent storyEvent; // TODO: supprimer ce champ si nécessaire
    
    [SerializeField]
    string npcName;
    
    [SerializeField]
    Sprite npcSprite;

    // public StoryEvent StoryEvent
    // {
        // get { return storyEvent; }
    // }
    
    public string NpcName
    {
        get { return npcName; }
    }
    public Sprite NpcSprite
    {
        get { return npcSprite; }
    }


    public void OnMouseDown()
    {
        HUD.Instance.SelectNPC(this);
    }
    
    public void Speak()
    {
        storyScene.SpeakWithActor(npcName);
        // TODO: changer si nécessaire avec:
        // storyScene.UseObject(this);
    }
    public void Show(string objectName)
    {
        Debug.Log("Montre l'objet " + objectName + " à " + NpcName);
        if (objectName == "Bloc-note" && NpcName == "Ted")
            // DAT HAKS !
            storyScene.Haxxxxxxxxx(StoryEvent.HAXXXXXX);
        storyScene.ShowObjectToNPC(this, objectName);
    }
}
