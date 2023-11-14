using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotebookManager : MonoBehaviour
{
    [Header("references")]
    [SerializeField] GameObject noteParent;
    [SerializeField] Image activityImage;
    [SerializeField] Sprite activeSprite;
    [SerializeField] Sprite inactiveSprite;

    [Header("notes")]
    [SerializeField] GameObject noteBase;

    public bool isActive = false;
    public void ToggleNotes()
    {
        SetVisibility(!isActive);
    }
    public void SetVisibility(bool isVisible)
    {
        isActive = isVisible;
        noteParent.SetActive(isVisible);
        activityImage.sprite = isVisible ? activeSprite : inactiveSprite;
    }
    public void AddLine()
    {
        GameObject newInstance = Instantiate(noteBase, noteParent.transform, false);
        newInstance.transform.localPosition = Vector3.zero;
        return;
    }
    private void Start()
    {
        SetVisibility(false);
    }
}
