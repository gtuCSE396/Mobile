using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallLocationHandler : MonoBehaviour
{

    //[SerializeField] private GameObject dataHolderObject;
    [SerializeField] private GameObject ballObject;
    [SerializeField] private GameObject textX;
    [SerializeField] private GameObject textY;
    [SerializeField] private GameObject textZ;

    //private DataHolder dHolder;
    private RectTransform ballTransform;

    private int originX;
    private int originY;

    private float originXPosition;
    private float originYPosition;

    private int index = 0;

    private float resizingRatio = 1f;

    float customTimer;

    // Start is called before the first frame update
    void Start()
    {
        ballTransform = ballObject.GetComponent<RectTransform>();

        originXPosition = ballTransform.position.x;     // Save initial position of the ball
        originYPosition = ballTransform.position.y;

        originX = 250;
        originY = 250;
    }

    // Update is called once per frame
    void Update()
    {   /*
        if (Time.fixedTime >= customTimer) 
        {
            int xDistance = (dHolder.xValues[index] - originX) / 2;     // Calculate the distance between origin and the ball
            int yDistance = (dHolder.yValues[index] - originY) / 2;

            ballTransform.position = new Vector3(originXPosition + xDistance, originYPosition + yDistance);     // Apply the calculated distance
            index++;
            if (index == dHolder.listMaxElements)       // If data list ends, start from the beginning
                index = 0;
            customTimer = Time.fixedTime + 0.5f;
        }*/


    }

    public void moveBall(float xPosition, float yPosition)
    {
        float xDistance = (xPosition - originX) / resizingRatio;     // Calculate the distance between origin and the ball
        float yDistance = (yPosition - originY) / resizingRatio;

        ballTransform.position = new Vector3(originXPosition + xDistance, originYPosition + yDistance);     // Apply the calculated distance
    }

    public void displayData(float xPosition, float yPosition, float zPosition)
    {
        textX.GetComponent<Text>().text = "X : " + xPosition;
        textY.GetComponent<Text>().text = "Y : " + yPosition;
        textZ.GetComponent<Text>().text = "Z : " + zPosition;
    }
}
