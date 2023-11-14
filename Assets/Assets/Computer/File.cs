using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class File : MonoBehaviour
{
    [Header("components")]
    public TMP_Text titleText;
    public TMP_Text extentionText;
    public Image iconImage;

    [Header("universal values")]
    public string title;
    public string extention;
    public string iconName;
    public string behaviour;

    [Header("specific values")]
    public TextAsset docBody;
    public Sprite image;

    [Header("sprites")]
    public Sprite document;
    public Sprite picture;
    public Sprite video;
    public Sprite music;



    UIPageManager pageManager;
    ComputerManager compManager;

    public void Click()
    {
        if (behaviour == "document")
        {
            compManager.OpenDoc(title, extention, docBody);
        } else if (behaviour == "picture")
        {
            compManager.OpenImage(title, extention, image);
        }
    }


    private void Start()
    {
        titleText.text = title;
        extentionText.text = extention;

        if (iconName == "document")
            iconImage.sprite = document;
        else if (iconName == "picture")
            iconImage.sprite = picture;
        else if (iconName == "video")
            iconImage.sprite = video;
        else if (iconName == "music")
            iconImage.sprite = music;

        pageManager = GetComponentInParent<UIPageManager>();
        compManager = pageManager.compManager;
    }



}
