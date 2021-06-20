using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesChanger : MonoBehaviour
{
    [System.Serializable]
    public class Rotater{
        public Sprite forward;
        public Vector3 forwardArms;
        public Sprite back;
        public Vector3 backArms;
        public Sprite left;
        public Vector3 leftArms;
        public Sprite rigth;
        public Vector3 rigthArms;
    }

    public Rotater rotater;
    public int normalLayer;
    SpriteRenderer sr;
    Rigidbody2D rb;
    Arms arms;

    void Awake() 
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        arms = GetComponent<Arms>();
    }

    void FixedUpdate() 
    {
        ChangeDirection(rb.velocity.x, rb.velocity.y);
    }

    void ChangeDirection(float x, float y)
    {
        if (Mathf.Abs(y) >= Mathf.Abs(x))
        {
            if (y > 0) ChangeParameters(rotater.back, normalLayer + 2, rotater.backArms);
            else if (y < 0) ChangeParameters(rotater.forward, normalLayer, rotater.forwardArms); 
        }
        else if (x > 0) ChangeParameters(rotater.rigth, normalLayer + 2, rotater.rigthArms);
        else if (x < 0) ChangeParameters(rotater.left, normalLayer, rotater.leftArms);
    }

    void ChangeParameters(Sprite sprite, int layer, Vector3 newArms)
    {
        arms.arms = newArms;
        sr.sprite = sprite;
        sr.sortingOrder = layer;
    }
}
