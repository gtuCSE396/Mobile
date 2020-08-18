using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogPanelCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject GraphCanvasGroupObject;
    [SerializeField] private GameObject BallLocationCanvasGroupObject;
    private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        hideAll();
    }
    public void showAll()
    {
        canvas.enabled = true;
        GraphCanvasGroupObject.GetComponent<GraphCanvasController>().hideAll();
        BallLocationCanvasGroupObject.GetComponent<BallLocationCanvasController>().hideAll();
    }

    public void hideAll()
    {
        canvas.enabled = false;
    }
}
