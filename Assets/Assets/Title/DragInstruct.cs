using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragInstruct : MonoBehaviour
{
    [SerializeField] GameObject moveObject;
    Vector3 ofset;

    public void StartDrag()
    {
        ofset = moveObject.transform.position - Input.mousePosition;
    }
    public void Drag()
    {
        Vector3 newPos = Input.mousePosition + ofset;

        newPos.x = Mathf.Max(0, Mathf.Min(newPos.x, 1920));
        newPos.y = Mathf.Max(0, Mathf.Min(newPos.y, 1080));

        moveObject.transform.position = newPos;
        return;
    }
}
