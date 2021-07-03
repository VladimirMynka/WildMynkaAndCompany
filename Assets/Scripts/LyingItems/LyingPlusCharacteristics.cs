using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LyingPlusCharacteristics : LyingItem
{
    public float plusHealth;
    public float plusRegeneration;
    public float plusMana;
    public float plusManaRegeneration;
    public float plusAttack;
    public float plusPower;
    public float plusProtection;
    public float plusAttackMagic;
    public float plusSelfMagic;
    public float plusStrangeMagic;
    public float plusCamouflage;

    protected override void OnActivate(GameObject creature)
    {
        Plus(creature);
    }

    void Plus(GameObject creature)
    {
        if (creature != player) return;
        var health = creature.GetComponent<Health>();
        var mana = creature.GetComponent<Mana>();
        var characteristics = creature.GetComponent<Characteristics>();
        health.maxHealth += plusHealth;
        health.regeneration += plusRegeneration;
        mana.maxMana += plusMana;
        mana.regeneration += plusManaRegeneration;
        characteristics.attack += plusAttack;
        characteristics.power += plusPower;
        characteristics.protection += plusProtection;
        characteristics.attackMagic += plusAttackMagic;
        characteristics.selfMagic += plusSelfMagic;
        characteristics.strangeMagic += plusStrangeMagic;
        characteristics.camouflage += plusCamouflage; 
        Destroy(gameObject);
    }
}