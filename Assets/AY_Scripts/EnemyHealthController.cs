using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private Image healthFill;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Gradient gradient;
    private float maxFillAmount;
    private int health;
    private float healthUpdate;
    private float maxValue;
    void Start()
    {
        health = 0;
        healthUpdate = 0.01f;
        maxFillAmount = 1f;
        maxValue = 1;
        healthFill.color = gradient.Evaluate(maxFillAmount);
    }

    void Update()
    {
        if (healthSlider != null && healthSlider.value != maxValue)
        {
            ModifySlider();
        }
    }

    private void ModifySlider()
    {
        if (healthSlider.value < maxValue)
        {
            healthSlider.value += healthUpdate;
        }
        else
        {
            healthSlider.value -= healthUpdate;
        }
        healthSlider.value = (float)System.Math.Round(healthSlider.value, 2);
        ChangeSliderColor();
    }

    private void ChangeSliderColor()
    {
        healthFill.color = gradient.Evaluate(healthSlider.normalizedValue);
    }

    public void SetEnemyHealth(int update)
    {
        health = update;
        maxFillAmount = (float)health;
        maxValue = (float)health / 100;
    }
}
