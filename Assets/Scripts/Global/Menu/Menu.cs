using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject saveMenu;
    public GameObject loadMenu;
    public GameObject applyCanvas;
    public float lastTimeScale;
    public bool open;
    [TextArea()]
    public string exitText;

    void Update() 
    {
        if(Input.GetKeyDown("escape")){
            if(open) closeMenu();
            else openMenu();
        }
    }
    void openMenu()
    {
        openMenu(gameObject);
        lastTimeScale = Time.timeScale;
        Time.timeScale = 0;
        open = true;
    }
    public void closeMenu(){
        closeMenu(gameObject);
        closeMenu(saveMenu);
        closeMenu(loadMenu);
        closeMenu(applyCanvas);
        Time.timeScale = lastTimeScale;
        open = false;
    }
    void openMenu(GameObject menu)
    {
        menu.GetComponent<Canvas>().planeDistance = 100;
    }
    void closeMenu(GameObject menu)
    {
        menu.GetComponent<Canvas>().planeDistance = -10;
    }
    public void openSaveMenu()
    {
        openMenu(saveMenu);
    }
    public void closeSaveMenu()
    {
        closeMenu(saveMenu);
    }
    public void openLoadMenu()
    {
        openMenu(loadMenu);
    }
    public void closeLoadMenu()
    {
        closeMenu(loadMenu);
    }
    public void openApplyCanvas()
    {
        openMenu(applyCanvas);
    }
    public void closeApplyCanvas()
    {
        closeMenu(applyCanvas);
    }
    public void beforeExitGame()
    {
        openApplyCanvas();
        applyCanvas.transform.Find("Text").gameObject.GetComponent<Text>().text = exitText;
        applyCanvas.transform.Find("YesButton").gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        applyCanvas.transform.Find("YesButton").gameObject.GetComponent<Button>().onClick.AddListener(() => { exitGame(); });
    }
    public void exitGame()
    {
        Application.Quit();
    }
}