using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<Item> inventory;
    public Dictionary<string, int> inventoryDictionary = new();
    public bool hasUnlockedBasement = false;
    public bool hasPlacedCandles;
    public int NumberOfCandles
    {
        get {
            int count = 0;
            if (GetItem("KitchenCandle").isUnlocked)
                count++;
            if (GetItem("TVCandle").isUnlocked)
                count++;
            if (GetItem("CeilingCandle").isUnlocked)
                count++;
            if (GetItem("BathroomCandle").isUnlocked)
                count++;
            if (GetItem("CurtainCandle").isUnlocked)
                count++;
            return count;
        }
        set { NumberOfCandles = value;  }
    }

    private void Awake()
    {
        // updates the index variable of each in inventory
        for (int i = 0; i < inventory.Count; i++)
        {
            inventoryDictionary.Add(inventory[i].name, i);
            inventory[i].index = i;
        }
    }
    public Item GetItem(string name)
    {
        return inventory[inventoryDictionary[name]];
    }
    public void EnableItem(string name)
    {
        GetItem(name).isUnlocked = true;
    }
}
