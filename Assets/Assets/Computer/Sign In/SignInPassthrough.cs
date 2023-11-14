using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignInPassthrough : MonoBehaviour
{
    [SerializeField] ComputerManager compManager;

    public void SetScreen(string screen)
    {
        compManager.SetScreen(screen);
    }
}
