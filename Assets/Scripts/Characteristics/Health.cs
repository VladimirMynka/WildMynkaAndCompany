using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update

    public float current;
    public float maxHealth;
    public float regeneration = 1;
    public SpriteRenderer sr;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (current <= 0){
            Destroy(gameObject);
        }

        current += Time.deltaTime * regeneration;
        if (current> maxHealth) current = maxHealth;

        float h = current / maxHealth;
        sr.color = new Color(
            sr.color[0],
            h,
            h,
            h
        );
    }
}