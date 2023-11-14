using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerManager : MonoBehaviour
{
    [SerializeField] Animator screenOff;
    [SerializeField] Animator signIn;

    [Header("screens")]
    [SerializeField] List<string> screenNames;
    [SerializeField] List<GameObject> screenObjects;

    [Header("windows")]
    [SerializeField] GameObject windowParent;
    [SerializeField] Vector3 defaultPos;
    [SerializeField] List<string> windowNames;
    [SerializeField] List<GameObject> windowObjects;

    public Dictionary<string, int> screens = new();
    public Dictionary<string, int> windows = new();

    public string correctPassword;
    public string enteredPass = "";
    public bool isOn = false;
    public InventoryManager invManager;
    public void OpenWindow(string windowName)
    {
        HandleOpenWindow(windowName);
    }
    public void OpenDoc(string docName, string docExtention, TextAsset docBody)
    {
        HandleOpenWindow("Notepad").GetComponent<NotePad>().Innitialize(docName, docExtention, docBody);
    }
    public void OpenImage(string docName, string docExtention, Sprite sprite)
    {
        HandleOpenWindow("ImageViewer").GetComponent<ImageViewer>().Innitialize(docName, docExtention, sprite);
    }
    public void ToggleScreen()
    {
        isOn = !isOn;
        screenOff.SetBool("IsActive", !isOn);
    }
    public void UpdateEntered(string pass)
    {
        enteredPass = pass;
    }
    public void CheckPassword()
    {
        if (enteredPass == correctPassword)
        {
            signIn.SetTrigger("SignIn");
        }
    }
    public void SetScreen(string screenName)
    {
        foreach (GameObject obj in screenObjects)
        {
            obj.SetActive(false);
        }
        screenObjects[screens[screenName]].SetActive(true);
    }
    public void ExitWithNewCode()
    {
        FindObjectOfType<PlayerSceneManager>().UnloadCompWithCode();
    }
    private GameObject HandleOpenWindow(string windowName)
    {
        GameObject newInstance = Instantiate(windowObjects[windows[windowName]], windowParent.transform, false);
        newInstance.transform.position += new Vector3(Random.Range(-25, 25), Random.Range(-25, 25), 0);
        return newInstance;
    }
    private void Awake()
    {
        invManager = FindObjectOfType<InventoryManager>();
        screenOff.gameObject.SetActive(true);
        for (int i = 0; i < screenNames.Count; i++)
        {
            screens.Add(screenNames[i], i);
        }
        for (int i = 0; i < windowNames.Count; i++)
        {
            windows.Add(windowNames[i], i);
        }
        SetScreen("Lock");
        correctPassword = invManager.gameObject.GetComponent<PlayerUIHandler>().correctComputerCode;
    }

}
