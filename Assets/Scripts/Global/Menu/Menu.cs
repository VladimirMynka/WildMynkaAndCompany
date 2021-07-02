using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    GameObject mainMenu;
    GameObject saveMenu;
    GameObject loadMenu;
    GameObject applyCanvas;
    public float lastTimeScale;
    public bool open;
    [TextArea()]
    public string exitText;

    void Awake() 
    {
        Canvases global = FindObjectOfType<Canvases>();
        mainMenu = global.mainMenu;
        saveMenu = global.saveMenu;
        loadMenu = global.loadMenu;
        applyCanvas = global.applyCanvas;
    }

    void Update() 
    {
        if(Input.GetKeyDown("escape")){
            if(open) closeMenu();
            else openMenu();
        }
    }
    void openMenu()
    {
        openMenu(mainMenu);
        lastTimeScale = Time.timeScale;
        Time.timeScale = 0;
        open = true;
    }
    public void closeMenu(){
        closeMenu(mainMenu);
        closeMenu(saveMenu);
        closeMenu(loadMenu);
        closeMenu(applyCanvas);
        Time.timeScale = lastTimeScale;
        open = false;
    }
    void openMenu(GameObject menu)
    {
        menu.SetActive(true);
    }
    void closeMenu(GameObject menu)
    {
        menu.SetActive(false);
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
        applyCanvas.transform.Find("Text").GetComponent<Text>().text = exitText;
        applyCanvas.transform.Find("YesButton").GetComponent<Button>().onClick.RemoveAllListeners();
        applyCanvas.transform.Find("YesButton").GetComponent<Button>().onClick.AddListener(() => { exitGame(); });
    }
    public void exitGame()
    {
        Application.Quit();
    }
}