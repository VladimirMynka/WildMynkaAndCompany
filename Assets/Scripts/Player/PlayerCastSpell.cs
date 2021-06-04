using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastSpell : MonoBehaviour
{
    public GameObject currentSpell;
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
    }

    void FixedUpdate()
    {
        if (spellTime != 0) spellTime += Time.deltaTime;
        if (audioTime != 0) audioTime += Time.deltaTime;
        if (spellTime > spellWaiting + 1) spellTime = 0;
        if (audioTime > audioWaiting + 1) audioTime = 0;

        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetButton("Fire1")) castSpell();
        if(Input.GetKeyDown("]")) nextSpell();
    }

    void castSpell(){
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
    void nextSpell(){
        if(!inventory) return;
        index++;
        if(index >= inventory.spells.Capacity) index = 0;
    }
}