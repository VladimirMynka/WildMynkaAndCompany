using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMagic : MonoBehaviour
{
    public float damage;
    public float time;
    float changePictureWaiting;
    public Sprite[] pictures;
    public int index;
    SpriteRenderer sr;
    void Start() {
        sr = GetComponent<SpriteRenderer>();
        changePictureWaiting = GetComponent<Spell>().aliveTime / pictures.Length;
        transform.position = GetComponent<Spell>().end;
    }

    void Update(){
        if (time >= changePictureWaiting + 1) time = 0;
        if (time != 0) time += Time.deltaTime;
        if (time == 0){
            index++;
            time++;
            sr.sprite = pictures[index];
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        GameObject otherGO = other.gameObject;
        if (otherGO == GetComponent< Spell >().owner) return;
        if (!otherGO) return;
        if (otherGO.GetComponent< Health >()){
            otherGO.GetComponent< Health >().current -= damage;
        }
    }
}
