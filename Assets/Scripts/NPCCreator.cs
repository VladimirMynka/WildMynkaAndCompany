using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCreator : MonoBehaviour
{
    [System.Serializable]
    public class NPCParameters
    {
        public float maxHealth;
        public float regeneration;
        public float attack;
        public float power;
        public float speed;
        public float attackMagic;
        public float selfMagic;
    }

    public GameObject[] prefabs;
    public GameObject[] items;
    float timer = 0;
    public float waiting;
    GameObject currentCreature;
    public GameObject miniCanvas;
    public GameObject dialogCanvas;
    public float radius;
    public NPCParameters beginParameters;
    public float beginKoefficient;
    float k = 1;


    void Update() 
    {
        if(timer == 0 && currentCreature == null)
        {
            CreateNPC();
        }
        if(timer >= waiting + 1) timer = 0;
        if(timer != 0) timer += Time.deltaTime;
    }

    void CreateNPC()
    {
        k *= beginKoefficient;
        timer = 1;
        int index = Random.Range(0, prefabs.Length);
        currentCreature = Instantiate(prefabs[index]);
        SetPosition(currentCreature);
        SetHealth(currentCreature);
        SetCharacteristics(currentCreature);
        SetTarget(currentCreature);
        SetAfterDeath(currentCreature);
        SetDialog(currentCreature);
    }

    void SetPosition(GameObject npc)
    {
        npc.transform.position = GetRandomPosition();
    }

    void SetHealth(GameObject npc)
    {
        var health = npc.GetComponent<Health>();
        health.maxHealth = beginParameters.maxHealth * k;
        health.current = beginParameters.maxHealth * k;
        health.regeneration = beginParameters.regeneration * k;
    }

    void SetCharacteristics(GameObject npc)
    {
        var characteristics = npc.GetComponent<Characteristics>();
        characteristics.attack = beginParameters.attack * k;
        characteristics.power = beginParameters.power * k;
        characteristics.attackMagic = beginParameters.attackMagic * k;
        characteristics.selfMagic = beginParameters.selfMagic * k;
    }

    void SetTarget(GameObject npc)
    {
        var target = npc.GetComponent<Target>();
        target.target = npc;
        target.speed = beginParameters.speed * k;
    }

    void SetAfterDeath(GameObject npc)
    {
        var afterDeath = npc.GetComponent<AfterDeath>();
        afterDeath.miniCanvas = miniCanvas;
        afterDeath.items = new GameObject[items.Length];
        for(int i = 0; i < items.Length; i++)
        {
            int probably = Random.Range(0, 100);
            if(probably < 5) afterDeath.items[i] = items[i];
        }
    }

    void SetDialog(GameObject npc)
    {
        var dialog = npc.GetComponentInChildren<Dialog>();
        dialog.miniCanvas = miniCanvas;
        dialog.dialogCanvas = dialogCanvas;
    }

    Vector3 GetRandomPosition()
    {
        float x = Random.Range(-radius, radius);
        float maxY = Mathf.Sqrt(radius * radius - x * x);
        float y = Random.Range(-maxY, maxY);
        return transform.position + new Vector3(x, y, 0);
    }
}