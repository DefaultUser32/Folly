using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField] Image floorObject;
    [SerializeField] List<GameObject> floorParents;
    [SerializeField] List<Sprite> floorSprites;

    public int floor;

    public readonly Dictionary<string, int> locationFloors = new()
    {
        { "Outside", 3 },
        { "Living", 1 },
        { "Dining", 1 },
        { "TV", 1 },
        { "Kitchen", 1 },
        { "BasementDoor", 1 },
        { "Laundry", 1 },
        { "Hallway", 1 },
        { "Master", 2 },
        { "Guest", 2 },
        { "Child", 2 },
        { "Bathroom", 2 },
        { "Office", 2 },
        { "BasementLeft", 0 },
        { "BasementRight", 0 },
        { "Cabinate", 1 },
        { "Void", 3 }
    };

    PlayerUIHandler UIHandler;

    public void SetFloor(string strFloor = "", int intFloor = -1) // sets map sprite in bottom left
    {
        // update current floor by name or index
        floor = intFloor;
        if (strFloor != "")
            floor = locationFloors[strFloor];

        // if floor == 3 (outside) disables map ui
        UIHandler.SetUIActivity(true, true);

        // enables/disables parent containing all buttons on a map layer
        for (int i = 0; i < floorParents.Count; i++)
        {
            floorParents[i].SetActive(i == floor);
        }

        // updates sprites for walls
        floorObject.sprite = floorSprites[floor];

    }
    private void Awake() // awake prevents map being set before references
    {
        UIHandler = GetComponent<PlayerUIHandler>();
    }
    private void Start()
    {
        SetFloor(strFloor : "Outside");
    }

}
