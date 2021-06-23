using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonation : MonoBehaviour
{
    public GameObject afterDetonation;
    public int quantity;
    public float radius;
    public float afterDetonationAliveTime;
    GameObject owner;

    void Start() {
        owner = GetComponent<Spell>().owner;
    }

    void OnDestroy() 
    {
        for (int i = 0; i < quantity; i++){
            float x = Random.Range(-radius, radius);
            float y = Mathf.Sqrt(radius*radius - x*x);
            y = Random.Range(-y, y);
            GameObject newFire = Instantiate(afterDetonation, transform.position + new Vector3(x, y, 0), transform.rotation);
            newFire.GetComponent<Spell>().owner = owner;
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        GameObject otherGO = other.gameObject;
        if (otherGO == owner) return;
        Health health = otherGO.GetComponent<Health>();
        if (health != null){
            Destroy(gameObject, 0.2f);
        }

    }
    
}