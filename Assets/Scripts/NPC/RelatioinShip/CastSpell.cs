using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpell : MonoBehaviour
{
    public bool enable;
    public GameObject currentSpell;
    public AudioClip spellAudio;
    public float audioWaiting;
    public float audioTime;
    public int index;
    public float changeSpellWaiting;
    float changeSpellTime;
    public float spellWaiting;
    float spellTime;
    public float spellDistance;
    public float spellNormalInDifference;
    public float spellNormalOutDifference;
    Inventory inventory;
    Target target;
    void Start()
    {
        inventory = GetComponent< Inventory >();
        target = GetComponent< Target >();
    }

    void FixedUpdate()
    {
        if (changeSpellTime != 0) changeSpellTime += Time.deltaTime;
        if (spellTime != 0) spellTime += Time.deltaTime;
        if (audioTime != 0) audioTime += Time.deltaTime;
        if (changeSpellTime > changeSpellWaiting + 1) changeSpellTime = 0;
        if (spellTime > spellWaiting + 1) spellTime = 0;
        if (audioTime > audioWaiting + 1) audioTime = 0;

        if(enable){
            changeSpell();
            castSpell();
        }
    }

    void castSpell(){
        if(spellTime != 0) return;
        if(!target) return;
        if(!target.target) return;
        if(target.distance - spellDistance > spellNormalOutDifference) return;
        if(spellDistance - target.distance > spellNormalInDifference) return;
        currentSpell = Instantiate(inventory.spells[index], transform.position, transform.rotation);
        
        Spell spell = currentSpell.GetComponent< Spell >();
        if (spell){
            spell.owner = gameObject;
            spell.begin = transform.position;
            spell.end = target.pointTarget;
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
    void changeSpell(){
        if (changeSpellTime != 0) return;
        if (!inventory) return;
        if (inventory.spells.Capacity <= 0) return;

        int length = inventory.spells.Capacity;
        int random = Random.Range(0, inventory.spells.Capacity);
        int i = 0;
        while (!inventory.spells[random] && i < length * 2){
            random = Random.Range(0, length);
            i++;
        }

        index = random;

        changeSpellTime += 1;
    }
}
