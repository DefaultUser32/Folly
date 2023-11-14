using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSceneManager : MonoBehaviour
{
    PlayerUIHandler playerUI;
    InventoryManager inventoryManager;
    public void LoadComputer()
    {
        playerUI.taskManager.SetCondition("hasFoundComputer");
        SceneManager.LoadScene("Computer", LoadSceneMode.Additive);
    }
    public void LoadPentagram()
    {
        SceneManager.LoadScene("Pentagram", LoadSceneMode.Additive);
    }
    public void LoadPhotoScene()
    {
        SceneManager.LoadScene("Photo", LoadSceneMode.Additive);
    }
    public void LoadEndScene()
    {
        SceneManager.LoadScene("Ending");
        FindObjectOfType<TimerManager>().EndTimer();
    }

    public void UnloadCompWithCode()
    {
        UnloadExtras();
        if (inventoryManager.GetItem("DecryptedCode").isUnlocked)
        {
            playerUI.SetText("The printer attempts to print another code.  It's clicking and complaining tells you it has nothing left to offer");
            return;
        }
        playerUI.SetText("A printer in the corner of the room whirs to life, spitting out a small slip of paper with a new code");
        playerUI.SetTextColour();
        inventoryManager.EnableItem("DecryptedCode");
    }
    public void UnloadPentagramWithAllCandles()
    {
        UnloadExtras();
        inventoryManager.hasPlacedCandles = true;
        playerUI.DoInteraction("Pentagram");
        playerUI.taskManager.SetCondition("hasDoneCandlePuzzle");
    }
    public void UnloadGlassManager()
    {
        UnloadExtras();
        BackgroundManager manager = GetComponent<BackgroundManager>();
        manager.isRuined = true;
        manager.SetLocation("Void");
        playerUI.HandleSwitchToRuin();        
    }
    public void UnloadExtras()
    {
        Scene computer = SceneManager.GetSceneByName("computer");
        Scene pentagram = SceneManager.GetSceneByName("pentagram");
        Scene photo = SceneManager.GetSceneByName("photo");
        if (computer.isLoaded)
            SceneManager.UnloadSceneAsync(computer);
        if (pentagram.isLoaded)
            SceneManager.UnloadSceneAsync(pentagram);
        if (photo.isLoaded)
            SceneManager.UnloadSceneAsync(photo);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnloadExtras();
        }
    }
    private void Start()
    {
        playerUI = GetComponent<PlayerUIHandler>();
        inventoryManager = GetComponent<InventoryManager>();
    }
}
