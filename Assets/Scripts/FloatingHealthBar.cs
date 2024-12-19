using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Gradient gradient;

    [SerializeField]
    private Image fill;

    public void UpdateHealthBar(float value, float maxValue)
    {
        slider.value = value / maxValue;
        fill.color = gradient.Evaluate(slider.value);
    }
}
