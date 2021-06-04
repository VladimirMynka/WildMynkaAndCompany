using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update

    public float health;
    public float maxHealth;
    public float regeneration = 1;

    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (health <= 0){
            Destroy(gameObject);
        }

        health += Time.deltaTime * regeneration;
        if (health > maxHealth) health = maxHealth;

        float h = health / maxHealth;
        GetComponent< SpriteRenderer >().color = new Color(
            GetComponent< SpriteRenderer >().color[0],
            h,
            h,
            h
        );
    }
}