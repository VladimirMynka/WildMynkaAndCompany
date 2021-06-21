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
    PlayerAttack playerAttack;
    Attack attack;
    GameObject weapon;
    bool inverse;

    void Awake() 
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        arms = GetComponent<Arms>();
        attack = GetComponent<Attack>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    void FixedUpdate() 
    {
        ChangeDirection(rb.velocity.x, rb.velocity.y);
        if (attack != null) weapon = attack.currentWeapon;
        else if(playerAttack != null) weapon = playerAttack.currentWeapon;

        if (weapon == null) return;

        if (inverse && weapon.transform.localScale.x > 0)
            weapon.transform.localScale -= new Vector3(weapon.transform.localScale.x * 2, 0, 0);
        if (!inverse && weapon.transform.localScale.x < 0)
            weapon.transform.localScale -= new Vector3(weapon.transform.localScale.x * 2, 0, 0);
    }

    void ChangeDirection(float x, float y)
    {
        if (Mathf.Abs(y) >= Mathf.Abs(x))
        {
            if (y > 0) ChangeParameters(rotater.back, normalLayer + 2, rotater.backArms, true);
            else if (y < 0) ChangeParameters(rotater.forward, normalLayer, rotater.forwardArms, false); 
        }
        else if (x > 0) ChangeParameters(rotater.rigth, normalLayer + 2, rotater.rigthArms, false);
        else if (x < 0) ChangeParameters(rotater.left, normalLayer, rotater.leftArms, true);
    }

    void ChangeParameters(Sprite sprite, int layer, Vector3 newArms, bool inv)
    {
        arms.arms = newArms;
        sr.sprite = sprite;
        sr.sortingOrder = layer;
        inverse = inv;
    }
}
