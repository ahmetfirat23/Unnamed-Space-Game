using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Response
{
    [TextArea(1, 5)]
    public string response;
    public int connectedID = -1;
    public bool eventTrigger = false;
}
