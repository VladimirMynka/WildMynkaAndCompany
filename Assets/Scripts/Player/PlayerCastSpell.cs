using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCastSpell : MonoBehaviour
{
    private const string SwitchEnableButton = "r";
    private const string SwitchSpellButton = "]";
    private const string CastSpellButton = "Fire1";
    
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
    public bool enable;
    float spellTime;
    public Vector3 target;
    Inventory inventory;
    private Characteristics characteristics;
    private Mana mana;
    SpriteRenderer sr;


    void Awake()
    {
        characteristics = GetComponent<Characteristics>();
        mana = GetComponent<Mana>();
        inventory = GetComponent<Inventory>();
        sr = GetComponent<SpriteRenderer>();
        if (inventory.spells.Count != 0){
            currentSpellText.GetComponent<Text>().text = inventory.spells[index].GetComponent<Spell>().spellName;  
            cursorExample = inventory.spells[index].GetComponent<Spell>().example;
            cursor = Instantiate(cursorExample, transform.position, transform.rotation);
            cursorSR = cursor.GetComponent<SpriteRenderer>();
            cursor.SetActive(enable);
        }
    }

    void Update()
    {
        if (spellTime != 0) spellTime += Time.deltaTime;
        if (audioTime != 0) audioTime += Time.deltaTime;
        if (spellTime > spellWaiting + 1) spellTime = 0;
        if (audioTime > audioWaiting + 1) audioTime = 0;

        if (cursor == null && inventory.spells.Count != 0)
        {
            currentSpellText.GetComponent<Text>().text = inventory.spells[index].GetComponent<Spell>().spellName;  
            cursorExample = inventory.spells[index].GetComponent<Spell>().example;
            cursor = Instantiate(cursorExample, transform.position, transform.rotation);
            cursorSR = cursor.GetComponent<SpriteRenderer>();
            cursor.SetActive(enable);
        }

        if (enable && cursor != null){
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
            cursor.transform.position = target;
            float alpha = (spellTime == 0) ? 1 : spellTime / (spellWaiting + 1);
            cursorSR.color = new Color(
                cursorSR.color[0],
                cursorSR.color[1],
                cursorSR.color[2],
                alpha
            );
            cursorSR.sortingLayerID = sr.sortingLayerID;
        }
        if(enable && Input.GetButton(CastSpellButton)) 
            CastSpell();
        if (Input.GetKeyDown(SwitchEnableButton))
        {
            ChangeState();
        }

        if(Input.GetKeyDown(SwitchSpellButton)){ 
            NextSpell();
        }
    }

    public void ChangeState()
    {
        if(inventory.spells.Count == 0) return;
        enable = !enable;
        if(cursorExample == null)
            cursorExample = inventory.spells[index].GetComponent<Spell>().example;
        if(cursor == null){
            cursor = Instantiate(cursorExample, transform.position, transform.rotation);
            cursorSR = cursor.GetComponent<SpriteRenderer>();
        }
        cursor.SetActive(enable);
    }

    void CastSpell()
    {
        if(spellTime != 0) return;
        if(inventory.spells.Count == 0) return;
        var spellToCopy = inventory.spells[index];
        if(spellToCopy.GetComponent<Spell>().ManaCost(characteristics) > mana.current)
            return;
        currentSpell = Instantiate(spellToCopy, transform.position, transform.rotation);
        
        Spell spell = currentSpell.GetComponent<Spell>();
        if (spell != null){
            mana.current -= spell.ManaCost(characteristics);
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
    public void NextSpell()
    {
        if(!inventory) return;
        index++;
        if(index >= inventory.spells.Count) index = 0;
        if (cursor != null) Destroy(cursor);
        cursorExample = inventory.spells[index].GetComponent<Spell>().example;
        cursor = Instantiate(cursorExample, transform.position, transform.rotation);
        cursorSR = cursor.GetComponent<SpriteRenderer>();
        cursor.SetActive(enable);
        currentSpellText.GetComponent<Text>().text = inventory.spells[index].GetComponent<Spell>().spellName;        
    }
}