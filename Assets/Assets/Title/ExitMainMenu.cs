using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMainMenu : MonoBehaviour
{
    [SerializeField] Animator fader;
    public void LoadNext()
    {
        FindObjectOfType<TimerManager>().StartTimer();
        SceneManager.LoadScene("House");
    }
    public void StartLoad()
    {
        fader.Play("LoadNextScene");
    }
}
