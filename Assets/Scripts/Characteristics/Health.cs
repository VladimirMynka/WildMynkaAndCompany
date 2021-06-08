using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update

    public float current;
    public float maxHealth;
    public float regeneration = 1;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (current <= 0){
            Destroy(gameObject);
        }

        current += Time.deltaTime * regeneration;
        if (current> maxHealth) current = maxHealth;

        float h = current / maxHealth;
        GetComponent< SpriteRenderer >().color = new Color(
            GetComponent< SpriteRenderer >().color[0],
            h,
            h,
            h
        );
    }
}