using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color high;
    public Vector3 offset;
    Health health;

    void Awake() {
        health = GetComponent<Health>();
        slider.transform.position = (transform.position + offset);
    }

    void Update()
    {
        SetHealth(health.current, health.maxHealth);
    }

    public void SetHealth(float health, float maxHealth)
    {
        slider.gameObject.SetActive(health < maxHealth);
        slider.value = health;
        slider.maxValue = maxHealth;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue); 
    }
}