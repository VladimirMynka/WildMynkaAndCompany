using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveMenuButtons : MonoBehaviour
{
    public GameObject buttonExample;
    GameObject saveMenu;
    GameObject saveContent;
    GameObject loadMenu;
    GameObject loadContent;
    GameObject applyCanvas;
    GlobalSaver globalSaver;
    public static readonly string SavesKey = "Saves";
    public static readonly string LastSaveKey = "LastSave";
    private int maxSave = 0;
    [TextArea()] public string saveText;
    [TextArea()] public string loadText;
    [TextArea()] public string newGameText;


    void Start()
    {
        Canvases global = FindObjectOfType<Canvases>();
        saveMenu = global.saveMenu;
        loadMenu = global.loadMenu;
        applyCanvas = global.applyCanvas;

        globalSaver = GetComponent<GlobalSaver>();
        saveContent = saveMenu.transform.Find("Image").Find("Saves").Find("Viewport").Find("Content").gameObject;
        loadContent = loadMenu.transform.Find("Image").Find("Loads").Find("Viewport").Find("Content").gameObject;

        getMaxSave();
        StartCoroutine(clearAndAddAll());       
    }

    void getMaxSave()
    {
        string saves = PlayerPrefs.GetString(SavesKey, "");
        MatchHandler handler = new MatchHandler(new Regex("~save(\\d+)").Match(saves));
        while (handler.hasNext())
        {
            int saveNumber = handler.nextInt();
            if (saveNumber > maxSave) maxSave = saveNumber;
        }
    }

    public void newSaveOnClick()
    {
        globalSaver.Save($"save{++maxSave}");
        StartCoroutine(clearAndAddAll());
        closeMenu(saveMenu);
    }

    IEnumerator clearAndAddAll()
    {
        clearSaveContent();
        clearLoadContent();
        yield return null;

        string saves = PlayerPrefs.GetString(SavesKey, "");
        MatchHandler handler = new MatchHandler(new Regex("~([^~]*)").Match(saves));

        while (handler.hasNext())
        {
            string saveName = handler.nextString();
            addSaveButton(saveName, "");
            addLoadButton(saveName, "");
        }
    }

    IEnumerator clearSaveContent()
    {
        for(int i = 1; i < saveContent.transform.childCount; i++){
            Destroy(saveContent.transform.GetChild(i).gameObject);
        }
        yield return null;
    }

    IEnumerator clearLoadContent()
    {
        for(int i = 0; i < loadContent.transform.childCount; i++){
            Destroy(loadContent.transform.GetChild(i).gameObject);
        }
        yield return null;
    }

    void addSaveButton(string saveName, string saveDate)
    {
        GameObject newSave = Instantiate(buttonExample) as GameObject;

        newSave.GetComponent< Button >().onClick.AddListener(() => { saveOnClick(saveName); });
        newSave.transform.Find("Name").gameObject.GetComponent<Text>().text = saveName;
        newSave.transform.Find("Date").gameObject.GetComponent<Text>().text = saveDate;

        newSave.transform.SetParent(saveContent.transform, false);
        newSave.transform.localPosition = saveContent.transform.GetChild(saveContent.transform.childCount - 2).transform.localPosition + new Vector3(0, -20, 0);
        newSave.transform.localScale = Vector3.one;
    }

    void addLoadButton(string saveName, string saveDate)
    {
        GameObject newLoad = Instantiate(buttonExample) as GameObject;

        newLoad.GetComponent< Button >().onClick.AddListener(() => { loadOnClick(saveName); });
        newLoad.transform.Find("Name").gameObject.GetComponent<Text>().text = saveName;
        newLoad.transform.Find("Date").gameObject.GetComponent<Text>().text = saveDate;

        newLoad.transform.SetParent(loadContent.transform, false);

        if (loadContent.transform.childCount > 1){
            newLoad.transform.localPosition = loadContent.transform.GetChild(loadContent.transform.childCount - 2).transform.localPosition + new Vector3(0, -20, 0);
        }
        else{
            newLoad.transform.localPosition = new Vector3(75, -20, 0);
        }        
        newLoad.transform.localScale = Vector3.one;
    }

    void saveOnClick(string saveName)
    {
        openMenu(applyCanvas);
        applyCanvas.transform.Find("Text").gameObject.GetComponent<Text>().text = saveText;
        applyCanvas.transform.Find("YesButton").gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        applyCanvas.transform.Find("YesButton").gameObject.GetComponent<Button>().onClick.AddListener(() => { save(saveName); });
    }
    void save(string saveName)
    {
        globalSaver.Save(saveName);
        StartCoroutine(clearAndAddAll());
        closeMenu(applyCanvas);
        closeMenu(saveMenu);
    }
    void loadOnClick(string saveName)
    {
        openMenu(applyCanvas);
        applyCanvas.transform.Find("Text").gameObject.GetComponent<Text>().text = loadText;
        applyCanvas.transform.Find("YesButton").gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        applyCanvas.transform.Find("YesButton").gameObject.GetComponent<Button>().onClick.AddListener(() => { load(saveName); });
    }
    void load(string saveName)
    {
        PlayerPrefs.SetString(LastSaveKey, saveName);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void newGame()
    {
        openMenu(applyCanvas);
        applyCanvas.transform.Find("Text").gameObject.GetComponent<Text>().text = newGameText;
        applyCanvas.transform.Find("YesButton").gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        applyCanvas.transform.Find("YesButton").gameObject.GetComponent<Button>().onClick.AddListener(() => { load(""); });

    }
    void openMenu(GameObject menu)
    {
        menu.SetActive(true);
    }
    void closeMenu(GameObject menu)
    {
        menu.SetActive(false);
    }

}
