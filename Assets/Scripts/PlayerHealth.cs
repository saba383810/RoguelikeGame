using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int playerHp=100;
    [SerializeField] private Text playerHpText;
    private Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        slider = GameObject.Find("PlayerHPSlider").GetComponent<Slider>();

        slider.maxValue = playerHp;
        slider.value = playerHp;
        playerHpText.text = slider.value.ToString("f0") + "/" + slider.maxValue.ToString("f0");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            damage();
        }
    }

    private void damage()
    {
        this.playerHp -= 1;
        slider.value = playerHp;
        playerHpText.text = slider.value.ToString("f0") + "/" + slider.maxValue.ToString("f0");
        return;
    }
}
