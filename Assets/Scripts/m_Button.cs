using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_Button : MonoBehaviour
{
    public GameObject helpCanvas;
    public GameObject dialogCanvas;
    
    public void helpCanvasOnClick(){
        helpCanvas.SetActive(false);
        Time.timeScale = 1;
    }
    public void dialogCanvasOnClick(){
        dialogCanvas.SetActive(false);
        Time.timeScale = 1;
    }
}