using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageViewer : MonoBehaviour
{
    public TMP_Text titleText;
    public Image mainImage;
    public void Innitialize(string title, string extension, Sprite sprite)
    {
        titleText.text = title + extension;
        mainImage.sprite = sprite;
    }
}
