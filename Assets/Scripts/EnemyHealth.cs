using System;
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
    private TextManager textManager;
    [SerializeField] private int damage=4;
    private void Start()
    {
        //slider = GameObject.Find("EnemySlider").GetComponent<Slider>();
        slider = GetComponentInChildren<Slider>();
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        textManager = GameObject.Find("TextManager").GetComponent<TextManager>();
        slider.maxValue = enemyHp;
        slider.value = enemyHp;
    }

    private void Update()
    {
        if (enemyHp <= 0)
        {
            Invoke(nameof(DelayDestroy), 0.5f);
            playerMove.ResetTargetObj();
        }
    }

    public void Damage()
    {
        if (gameObject.name == "Dummy") return;
        textManager.OutputLog(gameObject.name + "に" + damage + "のダメージ！");
        enemyHp -= damage;
        Invoke(nameof(SetSlider), 0.5f);
    }

    private void SetSlider()
    {
        slider.value = enemyHp;
    }
    private void DelayDestroy()
    {
        textManager.OutputLog(gameObject.name+"をたおした。");
        Destroy(gameObject);
    }

}
