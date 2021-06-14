using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class SaveMenuButtons : MonoBehaviour
{
    public GameObject buttonExample;
    public GameObject saveMenu;
    public GameObject saveContent;
    public GameObject loadMenu;
    public GameObject loadContent;
    GlobalSaver globalSaver;
    public static readonly string SavesKey = "Saves";
    public static readonly string LastSaveKey = "LastSave";
    private int maxSave = 0;

    void Start()
    {
        globalSaver = GetComponent<GlobalSaver>();
        saveContent = saveMenu.transform.Find("Image").Find("Saves").Find("Viewport").Find("Content").gameObject;
        loadContent = loadMenu.transform.Find("Image").Find("Loads").Find("Viewport").Find("Content").gameObject;

        getMaxSave();

    }

    void Update() {
        if(Input.GetKeyDown("escape")){
            if (loadMenu.GetComponent< Canvas >().planeDistance == 100){
                loadMenu.GetComponent< Canvas >().planeDistance = -10;
                Time.timeScale = 1;
            }
            else{
                StartCoroutine(clearAndAddAll());
                loadMenu.GetComponent< Canvas >().planeDistance = 100;
            }
        }
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
    }

    IEnumerator clearAndAddAll(){
        clearSaveContent();
        clearLoadContent();
        yield return new WaitForSeconds(0.05f);

        string saves = PlayerPrefs.GetString(SavesKey, "");
        MatchHandler handler = new MatchHandler(new Regex("~([^~]*)").Match(saves));

        while (handler.hasNext())
        {
            string saveName = handler.nextString();
            addSaveButton(saveName, "");
            addLoadButton(saveName, "");
        }
        Time.timeScale = 0;
    }

    void clearSaveContent(){
        for(int i = 1; i < saveContent.transform.childCount; i++){
            Destroy(saveContent.transform.GetChild(i).gameObject);
        } 
    }

    void clearLoadContent(){
        for(int i = 0; i < loadContent.transform.childCount; i++){
            Destroy(loadContent.transform.GetChild(i).gameObject);
        }
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
        globalSaver.Save(saveName);
    }

    void loadOnClick(string saveName)
    {
        globalSaver.Load(saveName);
    }

}
