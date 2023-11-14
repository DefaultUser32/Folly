using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetGame : MonoBehaviour
{
    public void LoadStart()
    {
        foreach (Image image in FindObjectsOfType<Image>())
        {
            image.color = Color.white;
        }
        foreach(TMP_Text text in FindObjectsOfType<TMP_Text>())
        {
            text.color = Color.white;
        }
        SceneManager.LoadScene("MainMenu");
    }
}
