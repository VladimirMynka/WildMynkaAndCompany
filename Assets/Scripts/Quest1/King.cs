using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class King : MonoBehaviour
{
    public AudioClip[] replicas;
    public GameObject portal;
    float time;
    float waiting;
    public float beginWaiting;
    int index;
    GameObject player;
    GameObject playerCopy;
    public float playerDistance;
    public float portalDistance;
    public float normalDistance;
    public GameObject helpCanvas;
    public GameObject subsCanvas;
    public GameObject weaponCanvas;
    public GameObject newSpell;
    public GameObject stone;
    public string[] helpStrings;
    public string[] subsStrings;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        time = 1;
        index = -2;
        waiting = beginWaiting;
    }

    void Update()
    {
        if(time != 0) time += Time.deltaTime;
        if(time > waiting + 1) time = 0;

        if(time == 0 && index == -2){
            helpCanvas.GetComponent< Canvas >().planeDistance = 100;
            helpCanvas.transform.Find("Text").GetComponent< Text >().text = helpStrings[0];
            Time.timeScale = 0;
            index++;
        }
        if(index == -1 && (player.transform.position - transform.position).magnitude <= normalDistance){
            index++;
        }
        if(time == 0 && index == 0){
            GetComponent< AudioSource >().PlayOneShot(replicas[0]);
            waiting = replicas[0].length / GetComponent< AudioSource >().pitch;
            subsCanvas.GetComponent< Canvas >().planeDistance = 100;
            subsCanvas.transform.Find("Image").Find("Text").GetComponent< Text >().text = subsStrings[0];
            index++;
            time++;
        }
        if(time == 0 && index == 1){
            GetComponent< AudioSource >().PlayOneShot(replicas[1]);
            waiting = replicas[1].length / GetComponent< AudioSource >().pitch;
            subsCanvas.transform.Find("Image").Find("Text").GetComponent< Text >().text = subsStrings[1];
            index++;
            time++;
            playerCopy = Instantiate(
                player, 
                player.transform.position + new Vector3(playerDistance, 0, 0), 
                player.transform.rotation
            );
        }
        if(time == 0 && index == 2){
            GetComponent< AudioSource >().PlayOneShot(replicas[2]);
            waiting = replicas[2].length / GetComponent< AudioSource >().pitch;
            subsCanvas.transform.Find("Image").Find("Text").GetComponent< Text >().text = subsStrings[2];
            index++;
            time++;
        }
        if(time == 0 && index == 3){
            subsCanvas.GetComponent< Canvas >().planeDistance = -10;
            helpCanvas.GetComponent< Canvas >().planeDistance = 100;
            helpCanvas.transform.Find("Text").GetComponent< Text >().text = helpStrings[1];
            Time.timeScale = 0;
            index++;
        }
        if(time == 0 && index == 4 && (!player || !playerCopy)){
            player = GameObject.FindWithTag("Player");
            GetComponent< AudioSource >().PlayOneShot(replicas[3]);
            waiting = replicas[3].length / GetComponent< AudioSource >().pitch;
            subsCanvas.GetComponent< Canvas >().planeDistance = 100;
            subsCanvas.transform.Find("Image").Find("Text").GetComponent< Text >().text = subsStrings[3];
            index++;
            time++;
        }
        if(time == 0 && index == 5){
            GetComponent< AudioSource >().PlayOneShot(replicas[4]);
            waiting = replicas[4].length / GetComponent< AudioSource >().pitch;
            subsCanvas.transform.Find("Image").Find("Text").GetComponent< Text >().text = subsStrings[4];
            index++;
            time++;
        }
        if(time == 0 && index == 6){
            GetComponent< AudioSource >().PlayOneShot(replicas[5]);
            waiting = replicas[5].length / GetComponent< AudioSource >().pitch;
            subsCanvas.transform.Find("Image").Find("Text").GetComponent< Text >().text = subsStrings[5];
            index++;
            time++;
        }
        if(time == 0 && index == 7){
            subsCanvas.GetComponent< Canvas >().planeDistance = -10;
            helpCanvas.GetComponent< Canvas >().planeDistance = 100;
            helpCanvas.transform.Find("Text").GetComponent< Text >().text = helpStrings[2];
            Time.timeScale = 0;
            portal.transform.position = transform.position + new Vector3(portalDistance, 0, 0);
            index++;
        }
        if(index == 8 && (player.transform.position - transform.position).magnitude > 10){
            weaponCanvas.GetComponent< Canvas >().planeDistance = 100;
            weaponCanvas.transform.Find("Image").Find("CurrentWeapon").gameObject.GetComponent<Text>().text = "сковорода";
            weaponCanvas.transform.Find("Image").Find("CurrentSpell").gameObject.GetComponent<Text>().text = "огонь";
            player.GetComponent< Inventory >().spells.Add(newSpell);
            index++;
        }
        if(time == 0 && index == 9){
            helpCanvas.GetComponent< Canvas >().planeDistance = 100;
            helpCanvas.transform.Find("Text").GetComponent< Text >().text = helpStrings[3];
            Time.timeScale = 0;
            index++;
        }
        if(index == 10 && !stone){
            helpCanvas.GetComponent< Canvas >().planeDistance = 100;
            helpCanvas.transform.Find("Text").GetComponent< Text >().text = helpStrings[4];
            Time.timeScale = 0;
            index++;
        }
    }
}
