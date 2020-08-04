using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Window_Graph : MonoBehaviour {

    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    private static List<int> valueList;
    private int counter;
    float customTimer;

    private void Awake() {

        customTimer = Time.fixedTime + 1.0f;
        counter = 1;

        graphContainer = gameObject.GetComponent<RectTransform>();      

        valueList = new List<int>() { 5, 15, 30, 45, 30, 22, 17, 15, 13, 17, 25, 37, 40, 36, 33 , 11 , 22, 35};
    }

    private void FixedUpdate()
    {
        if(Time.fixedTime >= customTimer) // Once in a second
        {
            if (counter != valueList.Count)
                counter++;
            else
            {
                if (GameObject.FindGameObjectsWithTag("mortal").Length > 0)
                {
                    GameObject[] mortals = GameObject.FindGameObjectsWithTag("mortal");
                    foreach (GameObject mortal in mortals)
                        GameObject.Destroy(mortal);
                }
                for (int i = 1; i < valueList.Count; i++)
                {
                    valueList[i - 1] = valueList[i];
                }
                valueList[valueList.Count - 1] = UnityEngine.Random.Range(1, 50);
            }
            ShowGraph(valueList);
            customTimer = Time.fixedTime + 1.0f;
        }
    }


    private GameObject CreateCircle(Vector2 anchoredPosition) {
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

    private void ShowGraph(List<int> valueList) {

        GameObject circle = GameObject.Find("circle");
        GameObject connection = GameObject.Find("dotConnection");

        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 100f;
        float xSize = 50f;

        GameObject lastCircleGameObject = null;
        for (int i = 0; i < counter; i++) {
            float xPosition = xSize + i * xSize;
            float yPosition = 10 + (valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null) {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;
        }
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB) {
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
