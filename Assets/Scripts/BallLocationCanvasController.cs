using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLocationCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject GraphCanvasGroupObject;
    [SerializeField] private GameObject LogPanelCanvasGroupObject;
    private Canvas canvas;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        //hideAll();
    }

    public void showAll()
    {
        canvas.enabled = true;
        GraphCanvasGroupObject.GetComponent<GraphCanvasController>().hideAll();
        LogPanelCanvasGroupObject.GetComponent<LogPanelCanvasController>().hideAll();
    }

    public void hideAll()
    {
        canvas.enabled = false;
    }
}
