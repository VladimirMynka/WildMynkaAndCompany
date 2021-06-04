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
    public GameObject newSpell;
    public GameObject stone;

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
            helpCanvas.transform.Find("Text").GetComponent< Text >().text = 
            "Отличный день, чтобы погрузиться в мир страшнейших безумий! Используйте клавиши W, A, S, D или стрелки для движения.";
            Time.timeScale = 0;
            index++;
        }
        if(index == -1 && (player.transform.position - transform.position).magnitude <= normalDistance){
            index++;
        }
        if(time == 0 && index == 0){
            GetComponent< AudioSource >().PlayOneShot(replicas[0]);
            waiting = replicas[0].length / GetComponent< AudioSource >().pitch;
            index++;
            time++;
        }
        if(time == 0 && index == 1){
            GetComponent< AudioSource >().PlayOneShot(replicas[1]);
            waiting = replicas[1].length / GetComponent< AudioSource >().pitch;
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
            index++;
            time++;
        }
        if(time == 0 && index == 3){
            helpCanvas.GetComponent< Canvas >().planeDistance = 100;
            helpCanvas.transform.Find("Text").GetComponent< Text >().text = 
            "Чтобы достать или убрать оружие, нажмите F. В боевом режиме ваш персонаж поворачивается за указателем мыши. Чтобы оружие причинило противнику вред, оно должно его коснуться. Выберите, кто вам нравится меньше, и опробуйте систему на нём. Постарайтесь, чтобы другой при этом выжил";
            Time.timeScale = 0;
            index++;
        }
        if(time == 0 && index == 4 && (!player || !playerCopy)){
            player = GameObject.FindWithTag("Player");
            GetComponent< AudioSource >().PlayOneShot(replicas[3]);
            waiting = replicas[3].length / GetComponent< AudioSource >().pitch;
            index++;
            time++;
        }
        if(time == 0 && index == 5){
            GetComponent< AudioSource >().PlayOneShot(replicas[4]);
            waiting = replicas[4].length / GetComponent< AudioSource >().pitch;
            index++;
            time++;
        }
        if(time == 0 && index == 6){
            GetComponent< AudioSource >().PlayOneShot(replicas[5]);
            waiting = replicas[5].length / GetComponent< AudioSource >().pitch;
            index++;
            time++;
        }
        if(time == 0 && index == 7){
            helpCanvas.GetComponent< Canvas >().planeDistance = 100;
            helpCanvas.transform.Find("Text").GetComponent< Text >().text = 
            "Подойдите к открывшемуся порталу. Находясь на нём, нажмите Space. Вы переместитесь в лучшее из мест, где можно побывать";
            Time.timeScale = 0;
            portal.transform.position = transform.position + new Vector3(portalDistance, 0, 0);
            index++;
        }
        if(index == 8 && (player.transform.position - transform.position).magnitude > 10){
            player.GetComponent< Inventory >().spells.Add(newSpell);
            index++;
        }
        if(time == 0 && index == 9){
            helpCanvas.GetComponent< Canvas >().planeDistance = 100;
            helpCanvas.transform.Find("Text").GetComponent< Text >().text = 
            "Добро пожаловать в Мынкинский математический лес - мир математики и диких мынок. Прыгая в портал, вы научились заклинанию - одному из многих. Для его использования зажмите клавишу ctrl или левую кнопку мыши. Вы можете опробовать его на камне справа";
            Time.timeScale = 0;
            index++;
        }
        if(index == 10 && !stone){
            helpCanvas.GetComponent< Canvas >().planeDistance = 100;
            helpCanvas.transform.Find("Text").GetComponent< Text >().text = 
            "Отлично! Теперь вы готовы к самостоятельным приключениям";
            Time.timeScale = 0;
            index++;
        }
    }
}
