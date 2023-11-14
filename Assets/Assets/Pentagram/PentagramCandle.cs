using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagramCandle : MonoBehaviour
{
    [SerializeField] PentagramManager manager;
    [SerializeField] public List<Vector3> positions;
    [SerializeField] public float lockDistance;

    public Vector3 boundryMin = Vector3.zero;
    public Vector3 boundryMax = new(1920, 1080, 0);
    Vector3 ofset;
    int? filledLocation = null;

    public void StartDrag()
    {
        ofset = transform.position - Input.mousePosition;
        if (filledLocation == null)
            return;
        manager.locationsAreFilled[(int)filledLocation] = false;
        filledLocation = null;
    }
    public void Drag()
    {
        Vector3 newPos = Input.mousePosition + ofset;
        newPos.x = Mathf.Max(boundryMin.x, Mathf.Min(newPos.x, boundryMax.x));
        newPos.y = Mathf.Max(boundryMin.y, Mathf.Min(newPos.y, boundryMax.y));
        
        transform.position = newPos;
        return;
    }
    public void StopDrag()
    {
        filledLocation = null;
        for (int i = 0; i < positions.Count; i++)
        {
            if (manager.locationsAreFilled[i])
                continue;

            filledLocation ??= i;

            float minOfset = Vector3.Distance(transform.localPosition, positions[(int)filledLocation]);
            float newOfset = Vector3.Distance(transform.localPosition, positions[i]);
            if (newOfset < minOfset)
                filledLocation = i;
        }
        if (filledLocation == null)
            return;
        if (Vector3.Distance(transform.localPosition, positions[(int)filledLocation]) < lockDistance)
        {
            transform.localPosition = positions[(int)filledLocation];
            manager.locationsAreFilled[(int)filledLocation] = true;
        }
        manager.CheckNumberFilled();
    }
}
