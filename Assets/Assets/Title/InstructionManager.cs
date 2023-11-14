using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionManager : MonoBehaviour
{
    [SerializeField] List<GameObject> protectedObjects;
    [SerializeField] List<GameObject> pages;
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] GameObject inventoryParent;
    int currentPage = 0;
    public void OpenInstructions()
    {
        currentPage = 0;
        inventoryParent.SetActive(true);
    } 
    public void CloseInstructions()
    {
        inventoryParent.SetActive(false);
    }
    public void UpdatePages()
    {
        foreach (GameObject page in pages) { page.SetActive(false);}
        pages[currentPage].SetActive(true);
    }
    public void SetPage()
    {
        if (dropdown.value == 0)
        {
            CloseInstructions();
            return;
        }
        OpenInstructions();
        currentPage = dropdown.value - 1;
        UpdatePages();
    }
    private void Start()
    {
        foreach(GameObject obj in protectedObjects)
        {
            DontDestroyOnLoad(obj);
        }
        CloseInstructions();
        SceneManager.LoadScene("MainMenu");
    }
}
