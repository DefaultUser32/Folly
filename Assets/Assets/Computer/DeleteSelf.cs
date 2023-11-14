using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteSelf : MonoBehaviour
{
    [SerializeField] GameObject parent;
    [SerializeField] Button fuck;
    [SerializeField] Image parentImage;
    [SerializeField] Image childImage;
    public void Delete()
    {
        Destroy(parent);
    }
    public void DisableSelf()
    {
        parentImage.enabled = false;
        childImage.enabled = false;
        fuck.enabled = false;
    }
}
