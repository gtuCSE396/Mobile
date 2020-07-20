using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class mainpage_button_on_click : MonoBehaviour
{
    public Button main_button;
    // Start is called before the first frame update
    void Start()
    {
        Button but = main_button.GetComponent<Button>();
        but.onClick.AddListener(task);
    }

    void task()
    {
        SceneManager.LoadScene("Start");
    }


}
