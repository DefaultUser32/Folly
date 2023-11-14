using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [SerializeField] Image box;
    [SerializeField] Sprite sel;
    [SerializeField] Sprite deSel;
    [SerializeField] TMP_Text timerText;
    bool isPaused = false;
    TimeSpan pauseTime;
    DateTime timerStart;
    bool hasEnded = true;

    Coroutine pause;
    public void StartTimer()
    {
        timerStart = DateTime.Now;
        hasEnded = false;
        pauseTime = TimeSpan.Zero;
        if (isPaused)
            Toggle();
    }
    public void EndTimer() 
    {
        hasEnded = true;
        if (!isPaused)
            Toggle();

    }
    public void Toggle()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            box.sprite = sel;
            pause = StartCoroutine(Pause());
            return;
        }
        StopCoroutine(pause);
        box.sprite = deSel;
    }
    public string GetElapsed()
    {
        if (timerStart == null)
            return "...";
        return (DateTime.Now - timerStart + pauseTime).ToString("mm':'ss");
    }
    private IEnumerator Pause()
    {
        DateTime lastUpdate = DateTime.Now;
        while (isPaused)
        {
            pauseTime += lastUpdate - DateTime.Now;
            lastUpdate= DateTime.Now;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
    private void Update()
    {
        if (hasEnded)
            return;
        timerText.text = GetElapsed();
    }
    
}
