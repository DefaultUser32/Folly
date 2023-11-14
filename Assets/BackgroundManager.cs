using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer backgroundObject;
    [SerializeField] Image backgroundFader;
    [SerializeField] List<string> roomDescriptions;
    [SerializeField] List<string> roomAltDescriptions;
    [SerializeField] List<bool> roomsHaveBeenVisited;
    [SerializeField] List<Sprite> backgrounds;
    [SerializeField] List<Sprite> backgroundsAlt;
    [SerializeField] List<SelectableObjectHolder> interactableParents;

    readonly Dictionary<string, int> locations = new()
    {
        { "Outside", 0 },
        { "Living", 1 },
        { "Dining", 2 },
        { "TV", 3 },
        { "Kitchen", 4 },
        { "BasementDoor", 5 },
        { "Laundry", 6 },
        { "Hallway", 7 },
        { "Master", 8 },
        { "Guest", 9 },
        { "Child", 10 },
        { "Bathroom", 11 },
        { "Office", 12 },
        { "BasementLeft", 13 },
        { "BasementRight", 14 },
        { "Cabinate", 15 },
        { "Void", 16 }
    };
    
    public float fadeSpeed;
    public string activeScene;
    public string nextScene;
    public int floor;
    public bool isRuined = false;
    public bool isFading = false;

    Coroutine fadeRoutine;
    MapManager mapManager;
    PlayerUIHandler uiHandler;
    TaskManager taskManager;

    public void SetLocation(string location) // handles all changes to do with moving location
    {
        // handle errors/invalid locations
        if (location == activeScene)
            return;
        if (GetSceneIndex(location) == -1)
        {
            Debug.LogError("ERROR " + location + " IS NOT VALID");
            return;
        }

        // clear UI
        uiHandler.DisablePreviews();
        uiHandler.ClearText();
        // update location
        nextScene = location;
        if (nextScene == "Living")
            taskManager.SetCondition("hasEnteredHouse");
        if (nextScene == "Kitchen")
            taskManager.SetCondition("hasEnteredKitchen");
        if (isRuined && nextScene == "BasementDoor")
            taskManager.SetCondition("hasFoundBasementDoor");
        if (nextScene == "Cabinate")
            taskManager.SetCondition("hasFoundSafe");


        // reset and start fade
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeBackground());
        taskManager.UpdateTasks();
    }
    public void SetActiveInteractables(string scene) // updates each interact parent's activity
    {
        int locIndex = GetSceneIndex(scene);
        for (int i = 0; i < interactableParents.Count; i++)
        {
            interactableParents[i].SetEnabled(locIndex == i, isRuined);
        }
    }
    public Sprite GetSceneSprite(string scene) // returns background sprite for given scene
    {
        if (isRuined)
            return backgroundsAlt[GetSceneIndex(scene)];
        return backgrounds[GetSceneIndex(scene)];
    }
    public int GetSceneIndex(string scene) // get index or return 0
    {
        if (locations.TryGetValue(scene, out int index)) 
            return index;
        return -1;
    }
    private void SetSceneVariables() // update scene (background, scene var, floor, interactables)
    {
        backgroundObject.sprite = GetSceneSprite(nextScene);

        activeScene = nextScene;

        mapManager.SetFloor(strFloor: nextScene);

        SetActiveInteractables(activeScene);

        if (isRuined)
            return;

        int roomIdx = locations[activeScene];

        if (roomsHaveBeenVisited[roomIdx])
            uiHandler.SetText(roomAltDescriptions[roomIdx]);
        else
            uiHandler.SetText(roomDescriptions[roomIdx]);

        roomsHaveBeenVisited[roomIdx] = true;

    }
    private IEnumerator FadeBackground() // animate the background fade
    {
        isFading = true;

        // innitialize background fade colour
        Color fadeColour = Color.white;
        fadeColour.a = 0;

        // increase alpha by 5% per itteration until solid
        for (int i = 0; i < 20; i++)
        {
            backgroundFader.color = fadeColour;
            fadeColour.a = i * 0.05f;
            yield return new WaitForSeconds(0.01f);
        }


        SetSceneVariables();


        // reverse fade
        for (int i = 20; i > 0; i--)
        {
            backgroundFader.color = fadeColour;
            fadeColour.a = i * 0.05f;
            yield return new WaitForSeconds(fadeSpeed);
        }

        // exit
        isFading = false;
        yield return null;
    }
    private void Start()
    {
        // reset visibility of background fader
        Color colour = Color.white;
        colour.a = 0;
        backgroundFader.color = colour;
        backgroundFader.enabled = true;

        // goto outside
        uiHandler = GetComponent<PlayerUIHandler>();
        mapManager = GetComponent<MapManager>();
        taskManager= GetComponent<TaskManager>();

        // set background without fading
        nextScene = "Outside";
        SetSceneVariables();
    }
}
