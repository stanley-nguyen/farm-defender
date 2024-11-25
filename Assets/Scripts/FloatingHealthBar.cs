using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    public void UpdateHealthBar(float value, float maxValue)
    {
        slider.value = value / maxValue;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
