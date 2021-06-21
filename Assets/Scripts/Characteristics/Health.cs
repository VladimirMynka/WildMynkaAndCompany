using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update

    public float current = 100;
    public float maxHealth = 100;
    public float regeneration = 1;

    void Update()
    {
        if (current <= 0){
            Destroy(gameObject);
        }

        if (current < maxHealth){
            current += Time.deltaTime * regeneration;
        }
        if (current >= maxHealth){
            current = maxHealth;
        }
    }
}