using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] TMP_Text titleTextBox;
    [SerializeField] TMP_Text descriptionTextBox;
    public List<Task> activeTasks;
    public List<Task> allTasks = new()
    {
        new ("Find John", "Nobody has seen him for months, you're searching the house for clues as to his whereabouts.", new()),
        new ("Search the House", "Walking through these familiar walls brings back memories, and hopefully information.", new() {"hasEnteredHouse"}),
        new ("Reassemble the Picture", "You can't pull your eyes away from that counter.  The shatered photo beckons you, begging you put it back together.", new List<string>() {"hasEnteredKitchen"}, new() {"hasEnteredRuin"}),
        new ("No Escape", "This was a mistake, you never should have come...  The doors are locked, windows sealed.  There is no way out.", new() {"hasEnteredRuin"}),
        new ("Get Into the Basement", "I swear that door was open when I got here.  Whatever is going on, the basement holds the key to getting out.", new() {"hasFoundBasementDoor"}),
        new ("Find Hidden Objects", "Use the blacklight to find objects invisible to the naked eye. (Look for objects that only glow under the light)", new() {"hasFoundBlacklight"}),
        new ("Decrypt Code", "The code seems useful, but first it needs to be converted into something legible", new() {"hasFoundEncryptedCode"}, new() {"hasFoundDecryptedCode", "hasFoundComputer"}),
        new ("Use Decrypted Code", "I need to find somewhere to input this code, like a SAFE or something", new () {"hasFoundDecryptedCode"}, new () {"hasFoundSafe"}),
        new ("Put Code Into the Safe", "The safe you found earlier seems a safe bet as to the use of the decrypted code", new () { "hasFoundDecryptedCode", "hasFoundSafe" }),
        new ("Blacklight Mystery", "Pointing the blacklight at the paintings in the hall reveals numbers, with boxes beneith them.", new() {"hasFoundBinaryPuzzle"}, new() { "hasFoundBinaryBook" }),
        new ("Binary Puzzle", "It becomes clear that using the blacklight on the paintings in the hallway has something to do with binary.", new( ) { "hasFoundBinaryPuzzle", "hasFoundBinaryBook" }),
        new ("Find the Lighter", "Ignite the candles, put the spirits to rest once more", new () {"hasDoneCandlePuzzle"}, new() {"hasFoundLighter"}),
        new ("Search the Computer", "John may have left information on his computer, I'd better check", new() { "hasFoundComputer" }),
        new ("Decrypt the Code", "Use the computer to decrypt the code", new() { "hasFoundDecryptedCode", "hasFoundComputer" }, new () {"hasFoundDecryptedCode"}),
        new ("Decryption Key", "We website asked you for a 5 didget decryption key, the paintings in the hallway may have something to do with it", new() { "hasFoundEncryptedCode", "hasFoundComputer", "hasFoundBinaryPuzzle" }, new() {"hasFoundDecryptedCode"}),
        new("", "", new())

    };

    public Dictionary<string, bool> conditions = new()
    {
        { "hasFoundBasementDoor", false},
        { "hasFoundBlacklight", false},
        { "hasFoundSafe", false},
        { "hasFoundEncryptedCode", false},
        { "hasFoundDecryptedCode", false },
        { "hasFoundBinaryBook", false},
        { "hasFoundComputer", false},
        { "hasFoundLighter", false},
        { "hasFoundACandle", false},
        { "hasEnteredRuin", false },
        { "hasEnteredKitchen", false },
        { "hasEnteredHouse", false },
        { "hasFoundBinaryPuzzle", false },
        { "hasDoneCandlePuzzle", false }
    };
    public void SetCondition(string condition)
    {
        conditions[condition] = true;
    }
    public void TaskComplete(string taskTitle)
    {
        foreach (Task task in activeTasks) {
            if (task.taskTitle == taskTitle)
                task.taskIsComplete = true;
        }
        UpdateTasks();
    }

    public void UpdateTasks()
    {
        UpdateConditions();
        UpdateDropdown();
        UpdateTaskText();
    }
    public void UpdateDropdown()
    {
        activeTasks = new();
        dropdown.ClearOptions();
        List<TMP_Dropdown.OptionData> newOptions = new();

        foreach (Task t in allTasks)
        {
            if (!CheckShowTask(t))
                continue;
            activeTasks.Add(t);

            newOptions.Add(new(t.taskTitle));
        }


        dropdown.AddOptions(newOptions);
        dropdown.RefreshShownValue();
    }
    public void UpdateTaskText()
    {
        descriptionTextBox.text = "";
        titleTextBox.text = "";

        Task currentTask = activeTasks.ElementAt<Task>(dropdown.value);

        titleTextBox.text = currentTask.taskTitle;
        descriptionTextBox.text = currentTask.taskDescription;
    }
    public void UpdateConditions()
    {
        if (inventoryManager.GetItem("Blacklight").isUnlocked)
            SetCondition("hasFoundBlacklight");
        if (inventoryManager.GetItem("BinaryBook").isUnlocked)
            SetCondition("hasFoundBinaryBook");
        if (inventoryManager.GetItem("Lighter").isUnlocked)
            SetCondition("hasFoundLighter");
        if (inventoryManager.GetItem("EncryptedCode").isUnlocked)
            SetCondition("hasFoundEncryptedCode");
        if (inventoryManager.GetItem("DecryptedCode").isUnlocked)
            SetCondition("hasFoundDecryptedCode");
        if (inventoryManager.NumberOfCandles > 0)
            SetCondition("hasFoundACandle");
    }
    
    public bool CheckShowTask(Task t)
    {
        if (t.taskIsComplete)
            return false;
        foreach (string condition in t.taskConditions)
        {
            if (!conditions[condition])
                return false;
        }
        foreach (string condition in t.taskNegations)
        {
            if (conditions[condition])
                return false;
        }
        return true;
    }
}
