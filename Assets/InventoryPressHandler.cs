using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class InventoryPressHandler : MonoBehaviour
{
    // class is used to pass information from inventory prefabs to rest of game
    // without having to manualy pass references
    [SerializeField] PlayerUIHandler handler;

    public void HandlePress(string text, string selfName, string associatedPreview, Sprite selfSprite)
    {
        if (selfName == "Computer Code")
        {
            handler.DoInteraction("ComputerCode");
        }
        if (handler.activePreview == "" || handler.activePreview != associatedPreview)
        {
            SetPreview(text, selfName, selfSprite);
            return;
        }
        handler.DoInteraction(selfName);

        handler.DisablePreviews();
        
    }
    public void SetPreview(string text, string selfName, Sprite selfSprite)
    {
        handler.SetText(text);
        handler.activePreview = selfName;
        handler.inventoryPreview.sprite = selfSprite;
        handler.inventoryPreview.enabled = true;
        handler.disablePreviewsAfterText = true;
        return;
    }
}
