using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObjectHolder : MonoBehaviour
{
    public bool onlyIfRuined;
    public void SetEnabled(bool isActive, bool isRuined)
    {
        // simple script used to disable/enable all selectable objects in a room
        if (!isActive)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(onlyIfRuined == isRuined);
    }
}
