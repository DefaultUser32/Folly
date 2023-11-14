using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotePad : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text bodyText;
    public void Innitialize(string title, string extension, TextAsset body)
    {
        titleText.text = title + extension;
        bodyText.text = body.ToString();
    }
}
