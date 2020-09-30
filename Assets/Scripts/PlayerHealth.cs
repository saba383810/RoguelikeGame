using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int playerHp=100;
    private Text playerHpText;
    private Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        slider = GameObject.Find("PlayerHPSlider").GetComponent<Slider>();
        playerHpText = GameObject.Find("HPText2").GetComponent<Text>();

        slider.maxValue = playerHp;
        slider.value = playerHp;
        playerHpText.text = slider.value.ToString("f0") + "/" + slider.maxValue.ToString("f0");
    }

    public void Damage()
    {
        this.playerHp -= 4;
        slider.value = playerHp;
        playerHpText.text = slider.value.ToString("f0") + "/" + slider.maxValue.ToString("f0");
        return;
    }

    private void Attack()
    {
        
    }
}
