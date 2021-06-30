using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject[] targets;
    public float timer;
    public float waiting;
    public GameObject[] spells;
    public GameObject[] points;
    public float pointStayTimer;
    public int currentPoint;
    Target target;
    GameObject player;

    private void Awake() 
    {
        target = GetComponent<Target>();
        player = GameObject.FindWithTag("Player");
    }

    private void Update() 
    {
        if ((points[currentPoint].transform.position - transform.position).sqrMagnitude < 1)
        {
            currentPoint++;
            if (currentPoint > points.Length) currentPoint = 0;
            target.target = points[currentPoint];
            int index = Random.Range(0, spells.Length);
            foreach(GameObject spellTarget in targets)
            {
                GameObject currentSpell = Instantiate(spells[index], transform.position, transform.rotation);
                
                Spell spell = currentSpell.GetComponent<Spell>();
                if (spell != null)
                {
                    spell.owner = gameObject;
                    spell.begin = transform.position;
                    spell.end = spellTarget.transform.position;
                }

                Target npcTarget = currentSpell.GetComponent<Target>();
                if (npcTarget != null)
                {
                    npcTarget.target = player;
                }
            }
        }
    }

}