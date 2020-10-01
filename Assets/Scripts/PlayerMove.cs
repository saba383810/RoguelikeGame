using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private static Vector3 MOVEX = new Vector3(2, 0, 0);
    private static Vector3 MOVEZ = new Vector3(0, 0, 2);

    private float step = 5f;
    private Vector3 moveTarget;
    private Vector3 prevpos;
    public Vector3 enemyPos;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private GameObject enemyObj;
    private Animator anim;
    private bool canAttack = true;
  
    
    
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
   
    private static readonly int IsAttack = Animator.StringToHash("isAttack");

    void Start()
    {
        moveTarget = transform.position;
        anim = GetComponent<Animator>();
        
        enemyObj = GameObject.Find("Dummy");
        playerHealth = gameObject.GetComponent<PlayerHealth>();

    }

    void Update()
    {
        if (this.transform.position == moveTarget)
        {
            SetTargetPosition();
        }
        //---------------攻撃----------------
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Mouse0)))
        {
            if (canAttack == true)
            {
                canAttack = false;
                enemyHealth = enemyObj.GetComponent<EnemyHealth>();
                anim.SetBool(IsAttack, true);
                enemyHealth.Damage();
                Invoke(nameof(SetIsAttackFalse), 0.5f);
            }
            else
            {
                anim.SetBool(IsAttack, true);
                Invoke(nameof(SetIsAttackFalse), 0.5f);
            }
        }

        Move();
        
    }
    //----------移動向きをセット------------------
    void SetTargetPosition()
    {
        prevpos = moveTarget;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            moveTarget = transform.position + MOVEX;
            return;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            moveTarget = transform.position - MOVEX;
            return;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            moveTarget = transform.position + MOVEZ;
            return;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            moveTarget = transform.position - MOVEZ;
            return;
        }
    }
    //----移動----
    private void Move()
    {
        var transform1=transform;
        var position = transform1.position;
        var direction = moveTarget - position;
        direction.y = 0;
        //アニメーション
        anim.SetBool(IsRunning, position != moveTarget);
        if (position != moveTarget)
        {
            var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);

            if (Input.GetKey(KeyCode.LeftShift) && enemyPos!=moveTarget)
            {
                transform.position = Vector3.MoveTowards(position, moveTarget, step * 2 * Time.deltaTime);
                playerHealth.FoodDamage(0.03f);
            }
            else if(enemyPos!=moveTarget)
            {
                transform.position = Vector3.MoveTowards(position, moveTarget, step * Time.deltaTime);
                playerHealth.FoodDamage(0.01f);
            }
            else
            {
                moveTarget = transform1.position;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.CompareTag("Enemy"))
        {
            var obj = other.gameObject;
            enemyPos = obj.transform.position;
            enemyObj = obj;
        }
    }
    private void OnTriggerExit(Collider other)
    {
            enemyObj = GameObject.Find("Dummy");
            enemyPos = enemyObj.transform.position;
    }

    private void SetIsAttackFalse()
    {
        anim.SetBool(IsAttack, false);
        canAttack = true;
    }

    public void ResetTargetObj()
    {
        enemyObj = GameObject.Find("Dummy");
        enemyPos = enemyObj.transform.position;
    }
}
