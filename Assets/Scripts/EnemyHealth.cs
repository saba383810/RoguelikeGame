﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int enemyHp =20;
    //private Slider slider;
    private Slider slider;
    PlayerMove playerMove;
    private void Start()
    {
        //slider = GameObject.Find("EnemySlider").GetComponent<Slider>();
        slider = GetComponentInChildren<Slider>();
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
        slider.maxValue = enemyHp;
        slider.value = enemyHp;
    }

    private void Update()
    {
        if (enemyHp <= 0)
        {
            Invoke(nameof(delayDestroy), 0.5f);
            playerMove.ResetTargetObj();
        }
    }

    public void Damage()
    {
        enemyHp -= 4;
        Invoke(nameof(SetSlider),0.5f);

    }

    private void SetSlider()
    {
        slider.value = enemyHp;
    }
    private void delayDestroy()
    {
        gameObject.SetActive(false);
        
    }

}
