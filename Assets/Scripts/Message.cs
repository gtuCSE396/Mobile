using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Message
{
    public string xCoor;
    public string yCoor;

    public Message(string xCoor, string yCoor)
    {
        this.xCoor = xCoor;
        this.yCoor = yCoor;
    }
}
