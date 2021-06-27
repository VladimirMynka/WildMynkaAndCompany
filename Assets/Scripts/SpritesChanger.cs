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
    SpriteRenderer sr;
    Rigidbody2D rb;
    Arms arms;
    Attack attack;
    GameObject weapon;
    bool inverse;

    void Awake() 
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        arms = GetComponent<Arms>();
        attack = GetComponent<Attack>();
    }

    void FixedUpdate() 
    {
        ChangeDirection(rb.velocity.x, rb.velocity.y);
        if (attack != null) weapon = attack.currentWeapon;
        ChangeWeaponDirection();
    }

    void ChangeDirection(float x, float y)
    {
        if (Mathf.Abs(y) >= Mathf.Abs(x))
        {
            if (y > 0.2) ChangeParameters(rotater.back, rotater.backArms, true);
            else if (y < -0.2) ChangeParameters(rotater.forward, rotater.forwardArms, false); 
        }
        else if (x > 0.2) ChangeParameters(rotater.rigth, rotater.rigthArms, false);
        else if (x < -0.2) ChangeParameters(rotater.left, rotater.leftArms, true);
    }

    void ChangeWeaponDirection()
    {
        if (weapon == null) return;
        var weaponSR = weapon.GetComponent<SpriteRenderer>();
        if (inverse && !weaponSR.flipX)
        {
            weaponSR.flipX = true;
            weaponSR.sortingOrder = sr.sortingOrder - 1;
        }
        if (!inverse && weaponSR.flipX)
        {
            weaponSR.flipX = false;
            weaponSR.sortingOrder = sr.sortingOrder;
        }
    }

    void ChangeParameters(Sprite sprite, Vector3 newArms, bool inv)
    {
        arms.arms = newArms;
        sr.sprite = sprite;
        inverse = inv;
    }
}
