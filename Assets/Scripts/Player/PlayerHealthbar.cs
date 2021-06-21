using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbar : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color high;
    Health health;

    void Awake() {
        health = GetComponent<Health>();
    }

    void Update()
    {
        SetHealth(health.current, health.maxHealth);
    }

    public void SetHealth(float health, float maxHealth)
    {
        slider.value = health;
        slider.maxValue = maxHealth;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue); 
    }
}