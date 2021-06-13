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
    GlobalSaver globalSaver;
    public static readonly string SavesKey = "Saves";
    public static readonly string LastSaveKey = "LastSave";
    private int maxSave = 0;

    void Start()
    {
        globalSaver = GetComponent<GlobalSaver>();
        saveContent = saveMenu.transform.Find("Image").Find("Saves").Find("Viewport").Find("Content").gameObject;

        getMaxSave();

    }

    void Update() {
        if(Input.GetKeyDown("escape")){
            if (saveMenu.GetComponent< Canvas >().planeDistance == 100){
                saveMenu.GetComponent< Canvas >().planeDistance = -10;
            }
            else{
                saveMenu.GetComponent< Canvas >().planeDistance = 100;
                StartCoroutine(clearAndAddAll());
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
        yield return new WaitForSeconds(0.1f);

        string saves = PlayerPrefs.GetString(SavesKey, "");
        MatchHandler handler = new MatchHandler(new Regex("~([^~]*)").Match(saves));

        while (handler.hasNext())
        {
            string saveName = handler.nextString();
            addSaveButton(saveName, "");
        }
        Time.timeScale = 0;
    }

    void clearSaveContent(){
        for(int i = 1; i < saveContent.transform.childCount; i++){
            Destroy(saveContent.transform.GetChild(i).gameObject);
        } 
    }

    void addSaveButton(string saveName, string saveDate)
    {
        GameObject newSave = Instantiate(buttonExample) as GameObject;

        newSave.GetComponent< Button >().onClick.AddListener(() => { saveOnClick(saveName); });
        newSave.transform.Find("Name").gameObject.GetComponent<Text>().text = saveName;
        newSave.transform.Find("Date").gameObject.GetComponent<Text>().text = saveDate;

        newSave.transform.parent = saveContent.transform;
        newSave.transform.localPosition = saveContent.transform.GetChild(saveContent.transform.childCount - 2).transform.localPosition + new Vector3(0, -20, 0);
        newSave.transform.localScale = Vector3.one;
    }

    void saveOnClick(string saveName)
    {
        globalSaver.Save(saveName);
    }

}
