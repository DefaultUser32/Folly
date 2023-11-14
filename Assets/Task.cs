using System.Collections;
using System.Collections.Generic;

public class Task
{
    public List<string> taskConditions;
    public List<string> taskNegations;
    public string taskTitle;
    public string taskDescription;
    public bool taskIsComplete;

    public Task(string _taskTitle, string _taskDescription, List<string> _taskConditions, List<string> _taskNegations = null) {
        taskTitle = _taskTitle;
        taskDescription = _taskDescription;
        taskConditions = _taskConditions;
        taskNegations = _taskNegations;
        taskNegations ??= new List<string>();
        taskIsComplete = false;
    }

}
