using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_Button : MonoBehaviour
{
    public GameObject helpCanvas;
    public GameObject dialogCanvas;
    
    public void helpCanvasOnClick(){
        helpCanvas.GetComponent< Canvas >().planeDistance = -10;
        Time.timeScale = 1;
    }
    public void dialogCanvasOnClick(){
        dialogCanvas.GetComponent< Canvas >().planeDistance = -10;
        Time.timeScale = 1;
    }
}