using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WritenNote : MonoBehaviour
{
    [Header("positional information")]
    [SerializeField] Vector2 boundryMin;
    [SerializeField] Vector2 boundryMax;

    Vector3 ofset;
    public void StartDrag()
    {
        ofset = transform.position - Input.mousePosition;
    }
    public void Drag()
    {
        Vector3 newPos = Input.mousePosition + ofset;

        newPos.x = Mathf.Max(boundryMin.x, Mathf.Min(newPos.x, boundryMax.x));
        newPos.y = Mathf.Max(boundryMin.y, Mathf.Min(newPos.y, boundryMax.y));

        transform.position = newPos;
        return;
    }
}
