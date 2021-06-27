using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAttack : Attack
{
    public AudioClip[] sounds;
    float changeWeaponTime;
    float attackTime;
    float audioTime;
    public float changeWeaponWaiting = 10;
    public float attackWaiting = 2;
    float audioWaiting;
    public float fixedAudioWaiting = 10;
    public float attackDistance = 0.3f;
    Target target;

    protected override void Awake()
    {
        base.Awake();
        target = GetComponent<Target>();
    }

    protected override void Update()
    {
        if (Time.timeScale == 0) return;
        base.Update();

        if (changeWeaponTime != 0) changeWeaponTime += Time.deltaTime;
        if (attackTime != 0) attackTime += Time.deltaTime;
        if (audioTime != 0) audioTime += Time.deltaTime;
        if (changeWeaponTime > changeWeaponWaiting + 1) changeWeaponTime = 0;
        if (attackTime > attackWaiting + 1) attackTime = 0;
        if (audioTime > audioWaiting + 1) audioTime = 0;

        if (changeWeaponTime == 0)
        {
            ChangeWeapon();
            changeWeaponTime += 1;
        }

        if (enable)
        {
            target.normalDistance = attackDistance;
            target.normalInDifference = attackDistance;
            target.normalOutDifference = 0;
        }

        if (attackTime == 0 && target.distance <= attackDistance && target.target != null)
        {
            Hit();
            attackTime += 1;
        }

        if(audioTime == 0 && sounds.Length > 0)
        {
            Say(Random.Range(0, sounds.Length));
            audioTime += 1;
        }
    }

    public override int NextIndex()
    {
        return Random.Range(0, inventory.weapons.Count);
    }

    void Say(int soundIndex)
    {
        GetComponent<AudioSource>().PlayOneShot(sounds[soundIndex]);
        audioWaiting = sounds[soundIndex].length + fixedAudioWaiting;
    }
}