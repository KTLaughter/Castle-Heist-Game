using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class healthbar : MonoBehaviour
{
    public GameBehavior gameBehavior;
    public Slider slowSlider;
    public float maxHealth = 100f;
    public float health;
    private float lerpSpeed = 0.005f;
    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        health = gameBehavior.Items;
        if(slowSlider.value != health)
        {
            slowSlider.value = Mathf.Lerp(slowSlider.value, health, lerpSpeed);
        }
    }
}
