using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private static Vector3 MOVEX = new Vector3(1, 0, 0);
    private static Vector3 MOVEZ = new Vector3(0, 0, 1);

    private float step = 2f;
    private Vector3 target;
    private Vector3 prevpos;
    private Animator anim;
    private static readonly int IsRunning = Animator.StringToHash("isRunning");

    // Start is called before the first frame update
    void Start()
    {
        //Invoke(nameof(getStartPos), 1.0f);
        target = transform.position;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position == target)
        {
            setTargetPosition();
        }
        move();
    }

    void getStartPos()
    {
        target = transform.position;
    }

    void setTargetPosition()
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
    void move()
    {
        Transform transform1;
        (transform1 = transform).LookAt(target);
        var position = transform1.position;
        anim.SetBool(IsRunning, position != target);

        transform.position = Vector3.MoveTowards(position, target, step * Time.deltaTime);
    }

    void lookAtTarget()
    {
        
    }
}
