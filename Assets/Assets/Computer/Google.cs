using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Google : MonoBehaviour
{
    [Header("key")]
    public string correctKey;

    [Header("encrypted code")]
    [SerializeField] Image codeView;
    [SerializeField] TMP_Text incorrect;
    [SerializeField] Sprite transparant;

    [Header("progress")]
    public bool isEncryptedCodeFilled = false;
    public bool isCorrectKey = false;

    ComputerManager compManager;
    InventoryManager invManager;

    private void Start()
    {
        compManager = GetComponent<UIPageManager>().compManager;
        invManager = compManager.invManager;  
    }
    public void SetPaperItem()
    {
        Item encryptedCode = invManager.GetItem("EncryptedCode");
        isEncryptedCodeFilled = encryptedCode.isUnlocked;
        if (isEncryptedCodeFilled)
        {
            codeView.sprite = invManager.GetItem("EncryptedCode").sprite;
            incorrect.enabled = false;
        }
        else
        {
            codeView.sprite = transparant;
            incorrect.enabled = true;
        }
    }
    public void SetEncryptionKey(string key)
    {
        isCorrectKey = key == correctKey;
    }
    public void CheckEntry()
    {
        if (isCorrectKey && isEncryptedCodeFilled)
        {
            compManager.ExitWithNewCode();
        }
    }
}
