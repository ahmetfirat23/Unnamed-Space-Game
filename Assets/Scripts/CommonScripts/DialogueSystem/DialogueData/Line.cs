using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Line
{
    public int id;
    public string name;
    [TextArea(2, 10)]
    public string line;
    public string color;
    public bool hasResponses = false;
    public Response[] responses;

}