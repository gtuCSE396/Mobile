using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimulationHandler : MonoBehaviour
{
    public DatabaseAPI database;
    
    public TMP_InputField xField;
    public TMP_InputField yField;

    public void SendMessage()
    {
        database.PostMessage(new Message(xField.text, yField.text), () =>
        {
            Debug.Log("Message was sent.\n");
        }, exception =>
        {
            Debug.Log(exception.Message);
        });
    }
}
