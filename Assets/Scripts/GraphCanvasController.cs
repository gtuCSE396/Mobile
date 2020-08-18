using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphCanvasController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject BallLocationCanvasGroupObject;
    [SerializeField] private GameObject LogPanelCanvasGroupObject;

    [SerializeField] private GameObject xCanvasObject;
    [SerializeField] private GameObject yCanvasObject;
    [SerializeField] private GameObject distanceCanvasObject;

    private Canvas xCanvas;
    private Canvas yCanvas;
    private Canvas distanceCanvas;

    private Canvas canvas;

    void Start()
    {
        xCanvas = xCanvasObject.GetComponent<Canvas>();
        yCanvas = yCanvasObject.GetComponent<Canvas>();
        distanceCanvas = distanceCanvasObject.GetComponent<Canvas>();
        
        canvas = GetComponent<Canvas>();

        showAll();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showAll()
    {
        canvas.enabled = true;
        showXGraph();
        BallLocationCanvasGroupObject.GetComponent<BallLocationCanvasController>().hideAll();
        LogPanelCanvasGroupObject.GetComponent<LogPanelCanvasController>().hideAll();
    }

    public void hideAll()
    {
        canvas.enabled = false;
    }

    public void showXGraph()
    {
        xCanvas.enabled = true;
        yCanvas.enabled = false;
        distanceCanvas.enabled = false;
    }

    public void showYGraph()
    {
        xCanvas.enabled = false;
        yCanvas.enabled = true;
        distanceCanvas.enabled = false;
    }
    public void showDistanceGraph()
    {
        xCanvas.enabled = false;
        yCanvas.enabled = false;
        distanceCanvas.enabled = true;
    }
}
