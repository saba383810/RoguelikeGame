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
    private Vector3 enemyPos;
    private EnemyHealth enemyHealth;
    private GameObject enemyObj;
    private Vector3 prevpos;
    private Animator anim;
    private bool canAttack = true;
    [SerializeField] private float food = 100;
    
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private Slider slider;
    private static readonly int IsAttack = Animator.StringToHash("isAttack");

    // Start is called before the first frame update
    void Start()
    {
        moveTarget = transform.position;
        anim = GetComponent<Animator>();
        slider = GameObject.Find("FoodSlider").GetComponent<Slider>();
        enemyObj = GameObject.Find("Dummy");
        slider.maxValue = food;
        slider.value = food;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position == moveTarget)
        {
            SetTargetPosition();
        }

        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Mouse0))&&canAttack==true)
        {
            canAttack = false;
            enemyHealth = enemyObj.GetComponent<EnemyHealth>();
            anim.SetBool(IsAttack, true);
            enemyHealth.Damage();
            Invoke(nameof(SetIsAttackFalse),0.5f);
        }
        
        Move();
        slider.value = food;
    }

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
                food -= 0.03f;
            }
            else if(enemyPos!=moveTarget)
            {
                transform.position = Vector3.MoveTowards(position, moveTarget, step * Time.deltaTime);
                food -= 0.01f;
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
        if (other.gameObject.transform.CompareTag("Enemy"))
        {
            enemyObj = GameObject.Find("Dummy");
            enemyPos = enemyObj.transform.position;
        }
    }

    private void SetIsAttackFalse()
    {
        anim.SetBool(IsAttack, false);
        canAttack = true;
    }

}
