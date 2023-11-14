using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPageManager : MonoBehaviour
{
    [SerializeField] string defaultPage;
    [SerializeField] Image background;
    [SerializeField] Vector2 boundryMin;
    [SerializeField] Vector2 boundryMax;
    [SerializeField] List<string> pageNames;
    [SerializeField] List<GameObject> pageObjects;
    [SerializeField] List<Sprite> pageSprites;

    public ComputerManager compManager;


    public Dictionary<string, int> pageObjectsDict = new();


    Vector3 ofset;

    private void Start()
    {
        compManager = FindObjectOfType<ComputerManager>();
        for (int i = 0; i < pageNames.Count; i++)
        {
            pageObjectsDict.Add(pageNames[i], i);
        }
        SetPage(defaultPage);
    }
    public void SetPage(string page)
    {
        int index = pageObjectsDict[page];
        foreach (GameObject obj in pageObjects)
        {
            obj.SetActive(false);
        }
        pageObjects[index].SetActive(true);
        background.sprite = pageSprites[index];
    }
    public void StartDrag()
    {
        ofset = transform.position - Input.mousePosition;
    }
    public void Drag()
    {
        Vector3 newPos = Input.mousePosition + ofset;

        newPos.x = Mathf.Max(boundryMin.x, Mathf.Min(newPos.x, boundryMax.x));
        newPos.y = Mathf.Max(boundryMin.y, Mathf.Min(newPos.y, boundryMax.y));

        transform.position = newPos;
        return;
    }
}
