using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private static Vector3 MOVEX = new Vector3(2, 0, 0);
    private static Vector3 MOVEZ = new Vector3(0, 0, 2);

    private float step = 5f;
    private Vector3 target;
    private Vector3 prevpos;
    private Animator anim;
    [SerializeField] private float food = 100;
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
        anim = GetComponent<Animator>();
        slider = GameObject.Find("FoodSlider").GetComponent<Slider>();
        slider.maxValue = food;
        slider.value = food;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position == target)
        {
            SetTargetPosition();
        }
        Move();
        slider.value = food;
    }

    void SetTargetPosition()
    {
        prevpos = target;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            target = transform.position + MOVEX;
            return;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            target = transform.position - MOVEX;
            return;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            target = transform.position + MOVEZ;
            return;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            target = transform.position - MOVEZ;
            return;
        }
    }

    private void Move()
    {
        
        var transform1=transform;
        var position = transform1.position;
        var direction =target - position;
        direction.y = 0;
        //アニメーション
        anim.SetBool(IsRunning, position != target);
        if (position != target)
        {
            
            var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);


            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.position = Vector3.MoveTowards(position, target, step * 2 * Time.deltaTime);
                food -= 0.03f;
            }
            else
            {
                transform.position = Vector3.MoveTowards(position, target, step * Time.deltaTime);
                food -= 0.01f;
            }
        }
    }
}
