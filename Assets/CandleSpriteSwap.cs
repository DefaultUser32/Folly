using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandleSpriteSwap : MonoBehaviour
{
    [SerializeField] Item relative;

    [Header("self references")]
    [SerializeField] Image selfImage;
    [SerializeField] Button buttonNoCandle;
    [SerializeField] Button buttonCandle;

    [Header("sprites")]
    [SerializeField] Sprite noCandle;
    [SerializeField] Sprite candle;
    public void Update()
    {
        buttonCandle.enabled = !relative.isUnlocked;
        buttonNoCandle.enabled = relative.isUnlocked;
        selfImage.sprite = relative.isUnlocked ? noCandle : candle;
    }
}
