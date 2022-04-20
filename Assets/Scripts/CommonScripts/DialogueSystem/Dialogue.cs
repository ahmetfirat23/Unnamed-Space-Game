using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

[System.Serializable]
public class Dialogue
{
    public DialogueData dialogueData;
    public DialogueBoxData[] dialogueBoxDatas;
    public UnityEvent[] eventsArray;
    public GameObject[] buttons;

    private Dictionary<string, UnityEvent> eventDict;

    public Dictionary<string, UnityEvent> ConnectEventsWithReplies()
    {
        eventDict = new Dictionary<string, UnityEvent>();
        foreach (Line line in dialogueData.lines)
        {
            if (line.hasResponses)
            {
                foreach (Response response in line.responses)
                {
                    if (response.eventTrigger)
                    {
                        eventDict.Add(response.response, eventsArray[eventDict.Keys.Count]);
                    }
                }
            }
        }
        return eventDict;
    }

    public DialogueBoxData GetDialogueBoxDataWithName(string name)
    {
        foreach(DialogueBoxData data in dialogueBoxDatas)
        {
            if(data.name.ToLower() == name.ToLower())
            {
                return data;
            }
        }

        throw new Exception("Invalid character name");
    }

    public void SetColors(string color, string name)
    {
        DialogueBoxData dialogueBoxData = dialogueBoxDatas[0];
        foreach (DialogueBoxData dbd in dialogueBoxDatas)
        {
            if (dbd.name == name)
            {
                dialogueBoxData = dbd;
            }
        }
        switch (color)
        {
            case "blue":
                dialogueBoxData.dialogueBox.GetComponent<Image>().sprite = dialogueBoxData.dialogueBoxColors[0];
                return;
            case "red":
                dialogueBoxData.dialogueBox.GetComponent<Image>().sprite = dialogueBoxData.dialogueBoxColors[1];
                return;
        }
    }
}



