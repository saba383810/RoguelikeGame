using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private bool canAttack = false; 
    [SerializeField] private float currentTime;
    private static readonly int IsBattle = Animator.StringToHash("isBattle");
    private static readonly int IsAttack = Animator.StringToHash("isAttack");
    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerHealth =  GameObject.Find("Player").GetComponent<PlayerHealth>();
        currentTime = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= 2.0f)
        {
            EnemyAttack();
            currentTime = 0;
            Invoke(nameof(SetAttackFalse),0.5f);
        }
        if (canAttack == false) currentTime = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        anim.SetBool(IsBattle,true);
        canAttack = true;
        var lookRotation = Quaternion.LookRotation(other.transform.position-this.transform.position, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);
    }
    
    private void OnTriggerExit(Collider other)
    {
        anim.SetBool(IsBattle,false);
        canAttack = false;
    }

    private void EnemyAttack()
    {
        anim.SetBool(IsAttack,true);
        playerHealth.HpDamage(4);

    }

    private void SetAttackFalse()
    {
        anim.SetBool(IsAttack,false);
    }
}
