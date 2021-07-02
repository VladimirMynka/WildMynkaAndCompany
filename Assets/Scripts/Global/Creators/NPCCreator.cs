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
    public float timer;
    public float waiting;
    public int index;
    public GameObject currentCreature;
    public float radius;
    public NPCParameters beginParameters;
    public float beginKoefficient;
    public float k = 1;
    int wait = 3;
    bool needWait = true;
    public float beginTimer;


    void Update() 
    {
        if(needWait)
        {
            if(timer < wait) timer += Time.deltaTime;
            if(timer > wait)
            { 
                timer = beginTimer;
                needWait = false;
            }
            return;
        }

        if(currentCreature == null)
        {
            if(timer == 0)
            { 
                k *= beginKoefficient;
                index = Random.Range(0, prefabs.Length);
                CreateNPC();
                timer = 1;
            }
            else timer += Time.deltaTime;
        }
        if(timer >= waiting + 1) timer = 0;
    }

    public void CreateNPC()
    {
        currentCreature = Instantiate(prefabs[index], transform.position, transform.rotation);
        currentCreature.name = gameObject.name + currentCreature.name;
        SetPosition(currentCreature);
        SetHealth(currentCreature);
        SetCharacteristics(currentCreature);
        SetTarget(currentCreature);
        SetAfterDeath(currentCreature);
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
        target.target = null;
        target.pointTarget = transform.position; 
        target.speed = beginParameters.speed * k;
    }

    void SetAfterDeath(GameObject npc)
    {
        var afterDeath = npc.GetComponent<AfterDeath>();
        afterDeath.items = new GameObject[items.Length];
        for(int i = 0; i < items.Length; i++)
        {
            int probably = Random.Range(0, 100);
            if(probably < 5) afterDeath.items[i] = items[i];
        }
    }

    Vector3 GetRandomPosition()
    {
        float x = Random.Range(-radius, radius);
        float maxY = Mathf.Sqrt(radius * radius - x * x);
        float y = Random.Range(-maxY, maxY);
        return transform.position + new Vector3(x, y, 0);
    }
}