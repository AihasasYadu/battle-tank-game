using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TankHealthController : MonoSingletonGeneric<TankHealthController>
{
    [SerializeField] private Image healthMeter;
    [SerializeField] private Gradient gradient;
    private float maxFillAmount;
    private int health;
    private float healthUpdate;
    private float maxValue;
    void Start()
    {
        health = 0;
        healthUpdate = 0.01f;
        maxFillAmount = 0.5f;
        healthMeter.fillAmount = 0f;
        maxValue = 1;
        gradient.Evaluate(2 * healthMeter.fillAmount);
        EventsManager.Health += SetHealthBar;
    }

    void Update()
    {
        if (healthMeter != null && !healthMeter.fillAmount.Equals(maxFillAmount))
        {
            ModifyMeter();
        }
    }

    private void ModifyMeter()
    {
        if(healthMeter.fillAmount < maxFillAmount)
        {
            healthMeter.fillAmount += healthUpdate;
        }
        else
        {
            healthMeter.fillAmount -= healthUpdate;
        }
        healthMeter.fillAmount = (float)System.Math.Round(healthMeter.fillAmount, 2);
        ChangeMeterColor();
    }

    private void ChangeMeterColor()
    {
        healthMeter.color = gradient.Evaluate(2 * healthMeter.fillAmount);
    }

    public void SetHealthBar(int update)
    {
        health = update;
        //dividing by 200 since max Health is 100 and max fill amount in inspector (as per the UI made) is 0.5
        maxFillAmount = (float)health / 200;
        maxValue = (float)health / 100;
    }
}
