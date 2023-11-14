using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Sprite sprite;
    public string itemName;
    public string associatedPreview;
    public string description;
    public int index;
    public bool isUnlocked;

    private void Start()
    {
        UpdateSelf();
    }
    public void UpdateSelf()
    {
        GetComponent<Image>().sprite = sprite;
        GetComponentInChildren<TMP_Text>().text = itemName;
    }
    public void Pressed()
    {
        GetComponentInParent<InventoryPressHandler>().HandlePress(description, itemName, associatedPreview, sprite);
    }
}
