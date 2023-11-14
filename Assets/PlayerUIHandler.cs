using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHandler : MonoBehaviour
{
    [Header("parents")]
    [SerializeField] GameObject UIParent;
    [SerializeField] TMP_Text textBox;

    [Header("inventory")]
    [SerializeField] GameObject inventoryParent;
    [SerializeField] int numberPerRow;
    [SerializeField] Vector3 basePosition;
    [SerializeField] float horizontalOfset;
    [SerializeField] float verticalOfset;

    [Header("tasks")]
    [SerializeField] GameObject taskParent;


    [Header("hide button")]
    [SerializeField] Slider hideSlider;
    [SerializeField] GameObject hideButton;
    [SerializeField] TMP_Text hideButtonText;

    [Header("previews")]
    [SerializeField] TMP_Text computerCode;
    [SerializeField] List<char> validCharacters;
    public string correctComputerCode;
    public string activePreview = "";
    public Image preview;
    public Image inventoryPreview;
    [SerializeField] List<string> previewNames;
    [SerializeField] List<string> previewText;
    [SerializeField] List<Sprite> previewSprites;

    [Header("other")]
    public float textSpeed;
    public float fadeSpeed;

    public bool isUIActive = false;
    public bool isInventoryOpen = false;
    public bool isTasksOpen = false;
    public bool disablePreviewsAfterText = false;

    public Dictionary<string, int> previewTextIndex = new();

    bool textFinished = false;

    Coroutine textRenderCoroutine;
    Coroutine fadeCoroutine;
    Image[] childrenImage;
    TMP_Text[] childrenText;


    public InventoryManager inventory;
    public BackgroundManager backgroundManager;
    public TaskManager taskManager;
    public PlayerSceneManager sceneManager;
    public void DoInteraction(string interactionObject) // used to handle logic of interactable items
    {
        DisablePreviews();
        string _interactionObject = interactionObject;
        bool openInventory = false;
        switch (_interactionObject)
        {
            case "LaundryShelf":
                if (inventory.GetItem("Blacklight").isUnlocked)
                {
                    SetText("Nothing left but chemicals");
                    return;
                }
                inventory.EnableItem("Blacklight");
                SetText("Amidst the bottles of cleaning supplies you find a blacklight flashlight.  You take it, deciding it may be useful");
                SetTextColour();
                return;
            case "BasementKeyhole":
                taskManager.SetCondition("hasFoundBasementDoor");
                if (inventory.hasUnlockedBasement)
                {
                    backgroundManager.SetLocation("BasementRight");
                    return;
                }
                if (inventory.GetItem("BasementKey").isUnlocked)
                    openInventory = true;
                break;
            case "EnteranceDoor":
                openInventory = true;
                break;
            case "House Key":
                backgroundManager.SetLocation("Living");
                return;
            case "Strange Key":
                taskManager.TaskComplete("Get Into the Basement");
                backgroundManager.SetLocation("BasementRight");
                inventory.hasUnlockedBasement = true;
                return;
            case "Safe":
                if (inventory.GetItem("BasementKey").isUnlocked)
                {
                    SetText("Reaching into the safe you find nothing but cobwebs and dust");
                    return;
                }
                if (inventory.GetItem("EncryptedCode").isUnlocked)
                    openInventory = true;
                break;
            case "Decrypted Code":
                SetText("Inside the safe you find a small brass key");
                SetTextColour();
                inventory.EnableItem("BasementKey");
                taskManager.TaskComplete("Put Code Into the Safe");
                return;
            case "Nightstand":
                if (inventory.GetItem("EncryptedCode").isUnlocked)
                {
                    _interactionObject = "NightstandFound";
                    SetText(previewText[previewTextIndex[_interactionObject]]);
                    SetTextColour();
                    return;
                }
                disablePreviewsAfterText = true;
                inventory.EnableItem("EncryptedCode");
                break;
            case "KitchenCandle":
                if (inventory.GetItem("KitchenCandle").isUnlocked)
                {
                    DisablePreviews();
                    SetText("An empty candle holder");
                    return;
                }
                DisablePreviews();
                SetText("An unidentifiable power emminates from the candle, you take it");
                SetTextColour();
                inventory.EnableItem("KitchenCandle");
                return;
            case "TVCandle":
                DisablePreviews();
                SetText("An unidentifiable power emminates from the candle, you take it");
                SetTextColour();
                inventory.EnableItem("TVCandle");
                return;
            case "CeilingLight":
                if (inventory.GetItem("CeilingCandle").isUnlocked)
                {
                    DisablePreviews();
                    SetText("Just a light");
                    return;
                }
                DisablePreviews();
                SetText("Pointing the blacklight at the ceiling reveals a shadow within the light.  Pulling it down finds you a candle, pulsing with a strange energy.");
                SetTextColour();
                inventory.EnableItem("CeilingCandle");
                return;
            case "BathroomCandle":
                if (inventory.GetItem("BathroomCandle").isUnlocked)
                {
                    DisablePreviews();
                    SetText("A towel, stiff with age");
                    return;
                }
                DisablePreviews();
                SetText("Next to the towel a candle draws your eye.  It's strange, seemingly glowing in the dark.  You grab it.");
                SetTextColour();
                inventory.EnableItem("BathroomCandle");
                return;
            case "CurtainCandle":
                if (inventory.GetItem("CurtainCandle").isUnlocked)
                {
                    DisablePreviews();
                    SetText("Nothing out of the ordinary.");
                    return;
                }
                DisablePreviews();
                SetText("An eerie glow diffuses through the curtains, giving them an ethereal radiance.  The source of which turns out to be an unusual candle.  You take it, sensing its power.");
                SetTextColour();
                inventory.EnableItem("CurtainCandle");
                return;
            case "LivingLeftShelf":
                if (inventory.GetItem("BinaryBook").isUnlocked)
                {
                    DisablePreviews();
                    SetText("More books");
                    return;
                }
                DisablePreviews();
                SetText("Out of all the books, one catches your eye.  Pulling it off the shelf causes it to fall open to a page on binary decoding.");
                SetTextColour();
                inventory.EnableItem("BinaryBook");
                return;
            case "Cloths":
                if (inventory.GetItem("Lighter").isUnlocked)
                {
                    DisablePreviews();
                    SetText("Why are there shirts out here?  And why are there only 3?");
                    return;
                }
                DisablePreviews();
                SetText("Digging your hands through the pockets nets you a lighter");
                SetTextColour();
                inventory.EnableItem("Lighter");
                return;
            case "Pentagram":
                if (!inventory.hasPlacedCandles)
                {
                    sceneManager.LoadPentagram();
                    return;
                }
                if (inventory.GetItem("Lighter").isUnlocked)
                {
                    sceneManager.LoadEndScene();
                    return;
                }
                SetText("My job is almost finished, I need to light the candles");
                return;
            case "CursedPhoto":
                if (!backgroundManager.isRuined)
                {
                    sceneManager.LoadPhotoScene();
                    return;
                }
                SetText("What has he done...");
                return;
            case "ComputerCode":
                computerCode.gameObject.SetActive(true);
                return;
            case "Rug":
                if (inventory.GetItem("ComputerCode").isUnlocked)
                {
                    SetText("Just a rug");
                    return;
                }
                inventory.GetItem("ComputerCode").description = "The Code is " + correctComputerCode;
                SetText("Underneath the carpet you find a sticky note");
                SetTextColour();
                  
                inventory.EnableItem("ComputerCode");
                return;
        }

        if(!previewTextIndex.TryGetValue(_interactionObject, out int previewIndex))
        {
            Debug.Log("NOT IN LIST OF PREVIEWS : " + _interactionObject);
            return;
        }

        activePreview = _interactionObject;
        preview.sprite = previewSprites[previewIndex];
        preview.enabled = true;

        for (int i = 0; i <= 5; i++)
        {
            if (activePreview == "Painting" + i)
                taskManager.SetCondition("hasFoundBinaryPuzzle");
        }


        if (openInventory)
        {
            ToggleInventory(true);
            ToggleInventory();
            return;
        } else
            SetText(previewText[previewIndex]);

        if (_interactionObject == "Nightstand")
            SetTextColour();
    }
    public void SetUIActivity(bool isOn, bool doHideButton) 
    {
        // set UI with passed parameters, hide "hideButton" seperately with doHideButton
        isUIActive = isOn;
        UIParent.SetActive(isOn);
        hideButton.SetActive(doHideButton);
    }
    public void HandleSwitchToRuin()
    {
        taskManager.SetCondition("hasEnteredRuin");
        taskManager.TaskComplete("Search the house");
        taskManager.TaskComplete("Reassemble the picture");
        taskManager.TaskComplete("Find John");
        StartCoroutine(SwitchToRuin());
    }
    public void ToggleHidden(bool forceActive = false)
    {
        // flips visibility, and updates "hideButton" text to be "Show UI" if the UI is hidden, and "Hide UI" otherwise
        if (isUIActive && forceActive) // exits if user wants ui active and it is already
        {
            return;
        }

        isUIActive = !isUIActive;

        if (forceActive) // force active if forceActive
        {
            isUIActive = true;
        }

        // start fade
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeUI(isUIActive));


        // update text
        hideButtonText.text = "Show UI";
        if (isUIActive)
            hideButtonText.text = "Hide UI";
    }
    public void ToggleInventory(bool forceClose = false)
    {
        // if inventory is open, disabled parent object
        // otherwise, activates and updates inventory
        ClearText();
        isInventoryOpen = !isInventoryOpen && !forceClose;
        inventoryParent.SetActive(isInventoryOpen);
        taskParent.SetActive(false);
        isTasksOpen = false;
        UpdateInventory();
    }
    public void UpdateHideUISlider()
    {
        Color newColour = Color.white;
        newColour.a = hideSlider.value;
        SetChildrenColour(newColour);
        if (newColour.a <  0.1) isUIActive= false;
        else isUIActive= true;
    }
    public void ToggleTasks(bool forceClose = false)
    {
        // if tasks are open, disabled parent object
        // otherwise, activates and updates tasks
        ClearText();
        isTasksOpen = !isTasksOpen && !forceClose;
        taskParent.SetActive(isTasksOpen);
        inventoryParent.SetActive(false);
        isInventoryOpen = false;
        taskManager.UpdateTasks();
    }
    public void SetText(string text)
    {
        // resets/starts text rendering coroutine (and forces inventory shut)
        disablePreviewsAfterText = false;

        hideButton.SetActive(true);
        ToggleHidden(forceActive: true);
        ToggleInventory(forceClose: true);
        ClearText();

        textFinished = false;

        textRenderCoroutine = StartCoroutine(RenderLine(text));
    }
    public void SetDisableAfterText(bool activity) // used in order to change things through button in editor
    {
        disablePreviewsAfterText = activity;
    }
    public void DisablePreviews()
    {
        preview.enabled = false;
        inventoryPreview.enabled = false;
        activePreview = "";
        computerCode.gameObject.SetActive(false);
    }
    public void UpdateInventory() // update inventory icons
    {
        // get all unlocked items in inventory, and disable ALL items
        List<Item> unlocked = new();
        foreach (Item item in inventory.inventory)
        {
            item.gameObject.SetActive(false);
            if (item.isUnlocked)
                unlocked.Add(item);
        }

        // enable all unlocked items in inventory and put them in place
        for (int i = 0; i < unlocked.Count; i++)
        {
            unlocked[i].gameObject.GetComponent<RectTransform>().anchoredPosition = basePosition +
                new Vector3(horizontalOfset * (i % 8), i >= numberPerRow ? verticalOfset : 0, 0);
            inventory.inventory[i].UpdateSelf();

            unlocked[i].gameObject.SetActive(true);
        }
    }
    public void ClearText()
    {
        if (textRenderCoroutine != null)
            StopCoroutine(textRenderCoroutine);
        textBox.color = Color.white;
        textBox.text = "";
        textFinished = true;
    }
    public void SetTextColour()
    {
        textBox.color = Color.yellow;
    }
    private IEnumerator SwitchToRuin()
    {
        SetText("...");
        yield return new WaitUntil(() => textFinished);
        SetText("As you slot the last piece into place, the house begins to shift");
        yield return new WaitUntil(() => textFinished);
        SetText("The walls creek, doors slam shut, and a cocofony of voices surround you");
        yield return new WaitUntil(() => textFinished);
        SetText("Time slips by before your eyes.");
        yield return new WaitUntil(() => textFinished);
        SetText("Walls deteriorating");
        yield return new WaitUntil(() => textFinished);
        SetText("Lights dimming");
        yield return new WaitUntil(() => textFinished);
        SetText("The world fading away");
        yield return new WaitUntil(() => textFinished);
        SetText("...");
        yield return new WaitUntil(() => textFinished);
        backgroundManager.SetLocation("Kitchen");
        SetText("You open your eyes, and everything has changed");
    }
    private IEnumerator RenderLine(string text)
    {
        // itterates over each character and adds it to the text, aswell as storing real time as of when char is printed
        // waits until "textSpeed" seconds has elapsed to start next char
        // however, if player presses mouse 0, immediately renders whole line
        textBox.text = "";
        float time;
        foreach (char c in text)
        {
            time = Time.realtimeSinceStartup;
            textBox.text += c;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0) || Time.realtimeSinceStartup - time > textSpeed);
            if (Input.GetMouseButtonDown(0))
            {
                textBox.text = text;
                yield return new WaitForEndOfFrame(); // prevents reading skip as exit
                break;
            }
        }
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        textBox.text = "";
        if (disablePreviewsAfterText)
            DisablePreviews();
        textBox.color = Color.white;
        textFinished = true;
        yield return null;
    }
    private IEnumerator FadeUI(bool fadeIn)
    {
        // enable parent
        UIParent.SetActive(true);

        // innitialize colour
        Color colour = Color.white;
        colour.a = fadeIn ? 0f : 1f;

        // if fade in, counts up from 0 adding 0.1 each time
        // otherwise starts at 1 and subtracts 0.1 each time
        for (int i = 0; i <= 10; i++)
        {
            colour.a = fadeIn ? (0.1f * i) : (1f - (0.1f * i));
            if (i == 10 && fadeIn)
                colour.a = 1f;
            SetChildrenColour(colour);
            yield return new WaitForSeconds(fadeSpeed);
        }

        // update parent and exit
        UIParent.SetActive(fadeIn);
        
        yield return null;
    }
    private void SetChildrenColour(Color colour) // sets colour of all children (text/image) to passed paremeter
    {
        // image children
        foreach(Image child in childrenImage)
        {
            child.color = colour;
        }
        // text children
        foreach(TMP_Text child in childrenText)
        {
            child.color = colour;
        }
        return;
    }
    private void Awake()
    {
        inventory = GetComponent<InventoryManager>();
        backgroundManager = GetComponent<BackgroundManager>();
        taskManager = GetComponent<TaskManager>();
        sceneManager = GetComponent<PlayerSceneManager>();
        childrenImage = UIParent.GetComponentsInChildren<Image>();
        childrenText = UIParent.GetComponentsInChildren<TMP_Text>();
        SetChildrenColour(Color.white);
        for (int i = 0; i < previewNames.Count; i++)
        {
            previewTextIndex.Add(previewNames[i], i);
        }
        correctComputerCode = "";
        for (int i = 0; i < 6; i++)
        {
            correctComputerCode += validCharacters[(int)Random.Range(0, validCharacters.Count - 1)];
        }
        computerCode.text = correctComputerCode;
    }
    private void Start()
    {
        ToggleInventory(forceClose: true);
        ToggleTasks(forceClose: true);
        DisablePreviews();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && activePreview != "") // close previews if clicking wont
        {
            DisablePreviews();
            ClearText();
        }
    }
}