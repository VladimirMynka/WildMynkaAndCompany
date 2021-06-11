using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCastSpell : MonoBehaviour
{
    public GameObject currentSpell;
    public GameObject cursorExample;
    public GameObject cursor;
    public SpriteRenderer cursorSR;
    public GameObject currentSpellText;
    public AudioClip spellAudio;
    public float audioWaiting;
    public float audioTime;
    public int index;
    public float spellWaiting;
    float spellTime;
    public Vector3 target;
    Inventory inventory;
    void Start()
    {
        inventory = GetComponent< Inventory >();
        currentSpellText.GetComponent<Text>().text = inventory.spells[index].GetComponent<Spell>().spellName;  
        cursorExample = inventory.spells[index].GetComponent<Spell>().example;
        cursor = Instantiate(cursorExample, transform.position, transform.rotation);
        cursorSR = cursor.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (spellTime != 0) spellTime += Time.deltaTime;
        if (audioTime != 0) audioTime += Time.deltaTime;
        if (spellTime > spellWaiting + 1) spellTime = 0;
        if (audioTime > audioWaiting + 1) audioTime = 0;

        target = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
        if (cursor != null){
            cursor.transform.position = target;
            float alpha = (spellTime == 0) ? 1 : spellTime / (spellWaiting + 1);
            cursorSR.color = new Color(
                cursorSR.color[0],
                cursorSR.color[1],
                cursorSR.color[2],
                alpha
            );
        }
        if(Input.GetButton("Fire1")) castSpell();
        if(Input.GetKeyDown("]")){ 
            nextSpell();
            currentSpellText.GetComponent<Text>().text = inventory.spells[index].GetComponent<Spell>().spellName;        
        }
    }

    void castSpell()
    {
        if(spellTime != 0) return;
        currentSpell = Instantiate(inventory.spells[index], transform.position, transform.rotation);
        
        Spell spell = currentSpell.GetComponent< Spell >();
        if (spell){
            spell.owner = gameObject;
            spell.begin = transform.position;
            spell.end = target;
            spellWaiting = spell.afterSpellWaiting;
            spellAudio = spell.sound;
            if (audioTime == 0){
                if (spellAudio){
                    audioWaiting = spellAudio.length;
                    audioTime += 1;
                    GetComponent< AudioSource >().PlayOneShot(spellAudio);
                }
            }
        }

        spellTime += 1;
    }
    void nextSpell()
    {
        if(!inventory) return;
        index++;
        if(index >= inventory.spells.Count) index = 0;
        if (cursor != null) Destroy(cursor);
        cursorExample = inventory.spells[index].GetComponent<Spell>().example;
        cursor = Instantiate(cursorExample, transform.position, transform.rotation);
        cursorSR = cursor.GetComponent<SpriteRenderer>();
    }
}