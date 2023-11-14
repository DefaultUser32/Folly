using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagramManager : MonoBehaviour
{
    public List<bool> locationsAreFilled = new() { false, false, false, false, false };
    public List<GameObject> candles;
    InventoryManager inventoryManager;
    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        int numberOfCandles = inventoryManager.NumberOfCandles;
        for (int i = 0; i < candles.Count; i++)
        {
            candles[i].SetActive(i < numberOfCandles);
        }
    }

    public void CheckNumberFilled()
    {
        foreach (bool location in locationsAreFilled)
        {
            if (!location) return;
        }
        FindObjectOfType<PlayerSceneManager>().UnloadPentagramWithAllCandles();
    }
}
