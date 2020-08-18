﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using System.Linq;

public class DrawGraph : MonoBehaviour {

    [SerializeField] private Sprite circleSprite;
    [SerializeField] private GameObject dataHolderObject;

    [SerializeField] private GameObject xGraphPanelObject;
    [SerializeField] private GameObject yGraphPanelObject;
    [SerializeField] private GameObject distanceGraphPanelObject;

    private RectTransform xGraphPanelContainer;
    private RectTransform yGraphPanelContainer;
    private RectTransform distanceGraphPanelContainer;

    private int counter = 1;
    private int dataHolderIndex = 0;
    private int queuesIndex = 0;
    private static int  logCounter = 0;
    float customTimer;

    private int maxGraphElement = 20;

    private int[] xQueue;
    private int[] yQueue;
    private int[] distanceQueue;

    private DataHolder dHolder;
    private static Text[] logs;

    private void Awake() {

        xQueue = new int[maxGraphElement];      // These arrays holds the current data on the graph
        yQueue = new int[maxGraphElement];
        distanceQueue = new int[maxGraphElement];

        dHolder = dataHolderObject.GetComponent<DataHolder>();
        xGraphPanelContainer = xGraphPanelObject.GetComponent<RectTransform>();
        yGraphPanelContainer = yGraphPanelObject.GetComponent<RectTransform>();
        distanceGraphPanelContainer = distanceGraphPanelObject.GetComponent<RectTransform>();

     
        customTimer = Time.fixedTime;
    }

    private void FixedUpdate()
    {
        if (Time.fixedTime >= customTimer) // Do the job inside this condition once in a second.
        {
            if (counter != maxGraphElement)
                counter++;

            if(counter == maxGraphElement)
            {
                shiftArrayLeft(xQueue);         // If maximum element on the graph exceeded, then shift the array to the left and put the element to the right-most space.
                shiftArrayLeft(yQueue);
                shiftArrayLeft(distanceQueue);

                if (GameObject.FindGameObjectsWithTag("mortal").Length > 0) // This condition kills the previous graph nodes and connections.
                {
                    GameObject[] mortals = GameObject.FindGameObjectsWithTag("mortal");
                    foreach (GameObject mortal in mortals)
                    GameObject.Destroy(mortal);
                }               
            }

            xQueue[queuesIndex] = dHolder.xValues[dataHolderIndex];         // Update the current data on the graph.
            yQueue[queuesIndex] = dHolder.yValues[dataHolderIndex];
            distanceQueue[queuesIndex] = dHolder.zValues[dataHolderIndex];

            dataHolderIndex++;

            if (dataHolderIndex == dHolder.listMaxElements)        // If data list ends, start from the beginning 
                dataHolderIndex = 0;

            if(queuesIndex != maxGraphElement - 1)
            {
                queuesIndex++;
            }

            ShowGraph(xQueue, xGraphPanelContainer);            // Display graphs on each panel individually.
            ShowGraph(yQueue, yGraphPanelContainer);
            ShowGraph(distanceQueue, distanceGraphPanelContainer);

            customTimer = Time.fixedTime + 0.5f;
        }
    }

    private GameObject CreateCircle(Vector2 anchoredPosition, RectTransform container) {         // Draws a node on the graph.
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(container, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        gameObject.tag = "mortal";
        return gameObject;
    }

    private void ShowGraph(int[] values, RectTransform container) {                       // Displays the graph.

        float graphHeight = container.rect.height;
        float yMaximum = 680f;
        float xSize = 50f;
        GameObject lastCircleGameObject = null;
        for (int i = 0; i < queuesIndex; i++) {
            float xPosition = xSize + i * xSize;
            float yPosition = 35 + (values[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), container);
            if (lastCircleGameObject != null) {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition, container);
            }
            lastCircleGameObject = circleGameObject;
        }
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB, RectTransform container) {      // Draws a connection between nodes.
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.tag = "mortal";
        gameObject.transform.SetParent(container, false);
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

    private void shiftArrayLeft(int [] arr)     // Shifts an array to the left.
    {
        for(int i = 1; i < arr.Count(); i++)
        {
            arr[i - 1] = arr[i];
        }
    }

}
