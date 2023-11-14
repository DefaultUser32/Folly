using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndManager : MonoBehaviour
{
    [SerializeField] Image menuButton;
    [SerializeField] TMP_Text menuText;
    [SerializeField] TMP_Text textbox;
    [SerializeField] List<string> textList;

    List<Image> images = new();
    List<TMP_Text> texts = new();
    private void Start()
    {
        Image[] _images = FindObjectsOfType<Image>();
        TMP_Text[] _texts = FindObjectsOfType<TMP_Text>();
        foreach (Image image in _images)
        {
            if (image != menuButton) 
                images.Add(image);
        }
        foreach (TMP_Text text in _texts)
        {
            if (text != menuText) 
                texts.Add(text);
        }
        StartCoroutine(DoEndAnim());
    }
    private IEnumerator DoEndAnim()
    {
        foreach (string text in textList) 
        {
            textbox.text = "";
            foreach (char character in text)
            {
                textbox.text += character;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(1.25f);
        }
        yield return new WaitForSeconds(1);
        Color newColor = Color.white;
        for (int i = 20; i >=0; i--)
        {
            newColor.a = i * 0.05f;
            foreach (Image child in images)
            {
                child.color = newColor;
            }
            foreach (TMP_Text text in texts)
            {
                text.color = newColor;
            }
            yield return new WaitForSeconds(0.05f);
        }
        for (int i = 0; i < 10; i++)
        {
            newColor.a = 0.1f * i;
            menuButton.color = newColor;
            menuText.color = newColor;
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }
}
