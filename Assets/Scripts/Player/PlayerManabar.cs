using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManabar : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color high;
    Mana mana;

    void Awake() {
        mana = GetComponent<Mana>();
    }

    void Update()
    {
        SetMana(mana.current, mana.maxMana);
    }

    public void SetMana(float mana, float maxMana)
    {
        slider.value = mana;
        slider.maxValue = maxMana;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue); 
    }
}