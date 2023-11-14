using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlacklightManager : MonoBehaviour
{
    [SerializeField] List<Image> revealObjects;
    [SerializeField] Image flashBackground;
    [SerializeField] Image flashCircle;

    public float fadeRange; // distance before completely transparent
    public float fadeMin; // minimum distance to object before it starts to fade
    public bool isFlashOn = false;

    Animator cirlceAnimation;
    InventoryManager inventory;
    Dictionary<Image, Image[]> revealWithChildren = new();
    private void Start()
    {
        // get references
        cirlceAnimation = flashBackground.gameObject.GetComponent<Animator>();
        inventory = GetComponent<InventoryManager>();
        for (int i = 0; i < revealObjects.Count; i++)
        {
            Image[] children = revealObjects[i].GetComponentsInChildren<Image>();
            if (children.Length > 0)
            {
                revealWithChildren.Add(revealObjects[i], children);
            }
        }

        // update all
        SetFlashActivity(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inventory.GetItem("Blacklight").isUnlocked) {
            SetFlashActivity(!isFlashOn);
        }
        if (isFlashOn)
            UpdateObjects();
    }
    public void SetFlashActivity(bool isActive) // enable/disable flashlight
    {
        isFlashOn = isActive;

        // update object visibility
        flashBackground.gameObject.SetActive(isActive);
        if (isActive)
            cirlceAnimation.SetBool("IsActive", isFlashOn);
        flashCircle.gameObject.SetActive(isActive);

        foreach (Image obj in revealObjects)
        {
            obj.gameObject.SetActive(isFlashOn);
        }
        

        // if flash is off, script finishes
        if (!isFlashOn)
        {
            return;
        }

        UpdateObjects();
    }
    public void UpdateObjects()
    {
        // update circle
        flashCircle.transform.position = Input.mousePosition;

        Vector3 distance;
        Color color = Color.white;
        foreach (Image obj in revealObjects)
        {
            
            // linerly interpolate between 1 (solid), and 0 (transparent) based on the distance between the object and the mouse
            distance = flashCircle.transform.position - obj.transform.position;
            color.a = 1 - Math.Max(0, distance.magnitude - fadeMin) / fadeRange;
            obj.color = color;
            if (!isFlashOn)
                color.a = 0f;

            revealWithChildren.TryGetValue(obj, out Image[] children);
            foreach (Image child in children)
            {
                child.color = color;
            }
        }
    }
}
