using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int playerHp=100;
    private Text playerHpText;
    private Slider hpSlider;
    private Slider foodSlider;
    [SerializeField] private float food = 100;
    // Start is called before the first frame update
    void Start()
    {
        hpSlider = GameObject.Find("PlayerHPSlider").GetComponent<Slider>();
        playerHpText = GameObject.Find("HPText2").GetComponent<Text>();
        foodSlider = GameObject.Find("FoodSlider").GetComponent<Slider>();
        hpSlider.maxValue = playerHp;
        hpSlider.value = playerHp;
        foodSlider.maxValue = food;
        foodSlider.value = food;
        playerHpText.text = hpSlider.value.ToString("f0") + "/" + hpSlider.maxValue.ToString("f0");
    }

    public void HpDamage(int damage)
    {
        this.playerHp -= damage;
        hpSlider.value = playerHp;
        playerHpText.text = hpSlider.value.ToString("f0") + "/" + hpSlider.maxValue.ToString("f0");
        return;
    }

    public void FoodDamage(float damage)
    {
        food -= damage;
        foodSlider.value = food;
    }
}
