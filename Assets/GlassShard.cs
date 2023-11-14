using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassShard : MonoBehaviour
{
    [SerializeField] GlassManager manager;
    [SerializeField] int index;
    public float lockRange;
    public Vector3 maxStartPos;
    public Vector3 minStartPos;
    Vector3 ofset;

    Vector3 boundryMin = Vector3.zero;
    Vector3 boundryMax = new(1920, 1080, 0);
    public void StartDrag()
    {
        manager.locationsAreFilled[index] = false;
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
    public void StopDrag()
    {
        if (Mathf.Abs(transform.localPosition.magnitude) < lockRange)
        {
            transform.localPosition = Vector3.zero;
            manager.locationsAreFilled[index] = true;
        }
    }
}
