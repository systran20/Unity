using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Bu Class Healtbar scripti kullanımı için yazılmıştır
/// Ömer ERMİŞ 19.03.2020
/// </summary>

public class HealtBarScript : MonoBehaviour
{
    private GameModes gameModes;
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    /// <summary>
    /// Sets the max value for slider component
    /// </summary>
    /// <param name="value"></param>
    public void SetMaxHealth(int value)
    {
        slider.maxValue = value;
        slider.value = value;
        fill.color= gradient.Evaluate(1f);  //Green color 1. stop
    }

    /// <summary>
    /// set the current slider value 
    /// </summary>
    /// <param name="value"></param>
    public void SetHealth(int value)
    {
        if (value>=0)
        {
            slider.value = value;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }        
    }
}
