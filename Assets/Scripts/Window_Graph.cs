using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Window_Graph : MonoBehaviour {

    [SerializeField] private Sprite circleSprite;
    [SerializeField] private int graphMode;     // 0 for X-Y, 1 for Z, 2 for log
    private GameObject graphPanel;
    private RectTransform graphContainer;
    private static List<int> x_y_values;
    private static List<int> z_values;
    private static int counter = 1;
    private static int  logCounter = 0;
    float customTimer;

    private static Text[] logs;

    private void Awake() {

        if (graphMode == 2)
        {
            logs = new Text[8];

            Text text1 = GameObject.Find("Log1").GetComponent<Text>();
            text1.text = "Mode: Test";
            text1 = GameObject.Find("Log2").GetComponent<Text>();
            logs[0] = text1;
            text1 = GameObject.Find("Log3").GetComponent<Text>();
            logs[1] = text1;
            text1 = GameObject.Find("Log3").GetComponent<Text>();
            logs[2] = text1;
            text1 = GameObject.Find("Log4").GetComponent<Text>();
            logs[3] = text1;
            text1 = GameObject.Find("Log5").GetComponent<Text>();
            logs[4] = text1;
            text1 = GameObject.Find("Log6").GetComponent<Text>();
            logs[5] = text1;
            text1 = GameObject.Find("Log7").GetComponent<Text>();
            logs[6] = text1;
            text1 = GameObject.Find("Log8").GetComponent<Text>();
            logs[7] = text1;
        }

        customTimer = Time.fixedTime;
        if (graphMode == 0) // X-Y Mode
        {
            graphPanel = GameObject.Find("X-Y-Graphic");
        }
        else if(graphMode == 1) // Z mode
        {
            graphPanel = GameObject.Find("Z-Graphic");
        }

        if(graphMode == 0 || graphMode == 1)
        {
            graphContainer = graphPanel.GetComponent<RectTransform>();
        }
        x_y_values = new List<int>() { 5, 15, 30, 45, 30, 22, 17, 15, 13, 17, 25, 37, 40, 36, 33, 11, 22, 35 }; // Initial values to process in graph
        z_values = new List<int>() { 0, 0, 10, 20, 30, 20, 10, 0, 10, 20, 30, 40, 50, 60, 70, 60, 50, 40, 30, 20, 10, 0, 10, 20, 10, 0, 10, 20, 30, 40, 30, 20, 10, 0 };

    }

    private void FixedUpdate()
    {
        if (Time.fixedTime >= customTimer) // Do the job inside this condition once in a second.
        {
            Debug.Log("Counter " + counter);
            if (counter != x_y_values.Count)
                counter++;

            if(counter == x_y_values.Count)
            {
                int backup;
                if (GameObject.FindGameObjectsWithTag("mortal").Length > 0) // This condition kills the previous graph nodes and connections.
                {
                    GameObject[] mortals = GameObject.FindGameObjectsWithTag("mortal");
                    foreach (GameObject mortal in mortals)
                    GameObject.Destroy(mortal);
                }
                backup = z_values[0];
                for (int i = 1; i < z_values.Count; i++)   // This condition shifts the graph when the panel is full.
                {
                    z_values[i - 1] = z_values[i];
                }
                z_values[z_values.Count - 1] = backup;   // Assigns random value to the last item for simulation purposes.
                for (int i = 1; i < x_y_values.Count; i++)   // This condition shifts the graph when the panel is full.
                {
                    x_y_values[i - 1] = x_y_values[i];
                }
                x_y_values[x_y_values.Count - 1] = UnityEngine.Random.Range(1, 50);   // Assigns random value to the last item for simulation purposes.
            }

            if (graphMode == 0) // X-Y Mode
                ShowGraph(x_y_values);
            else if (graphMode == 1) // Z Mode
                ShowGraph(z_values);

            if (graphMode == 2)
            {
                int hour;
                int minutes;
                int seconds;
                hour = System.DateTime.Now.Hour;
                minutes = System.DateTime.Now.Minute;
                seconds = System.DateTime.Now.Second;
                

                
                if(logCounter == 8)
                {
                    for(int i = 1; i < 8; i++)
                    {
                        logs[i - 1].text = logs[i].text;
                     }
                    logs[7].text = hour + ":" + minutes + ":" + seconds;
                    logs[7].text += " X = ? " + "Y = " + x_y_values[counter - 1] + " Z = " + z_values[counter - 1];
                }
                else
                {
                    logs[logCounter].text = hour + ":" + minutes + ":" + seconds;
                    logs[logCounter].text += " X = ? " + "Y = " + x_y_values[counter - 1] + " Z = " + z_values[counter - 1];
                    logCounter++;
                }
                
                
                
            }

            

            customTimer = Time.fixedTime + 1.0f;
        }
    }

    private GameObject CreateCircle(Vector2 anchoredPosition) {         // Draws a node on the graph.
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        gameObject.tag = "mortal";
        return gameObject;
    }

    private void ShowGraph(List<int> values) {                       // Displays the graph.

        GameObject circle = GameObject.Find("circle");
        GameObject connection = GameObject.Find("dotConnection");

        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 100f;
        float xSize = 50f;

        GameObject lastCircleGameObject = null;
        for (int i = 0; i < counter; i++) {
            float xPosition = xSize + i * xSize;
            float yPosition = 20 + (values[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null) {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;
        }
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB) {      // Draws a connection between nodes.
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.tag = "mortal";
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(0,0,0, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }

}
