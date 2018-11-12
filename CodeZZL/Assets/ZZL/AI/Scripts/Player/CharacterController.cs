using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : PhysicsObject
{
    public float rotateSpeed = 10.0f;

    public float jumpTakeOffSpeed = 7.0f;
    public float maxSpeed = 7.0f;

    private bool m_canRotate = false;

    private Animator m_animator;

    private float m_currentFlag = 0.0f;

	void Awake ()
    {
        m_animator = GetComponent<Animator>();
        m_animator.SetFloat("jump_take_off_speed", jumpTakeOffSpeed);
    }

    //void FixedUpdate()
    //{
    //    OnMove();        
    //}

    protected override void OnRotate()
    {
        if(m_canRotate)
        {
            transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f) * rotateSpeed, 180);
        }
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        //Debug.Log(move.x);

        //if (move.x == 0)
        //{
        //    isMoving = false;
        //}
        //else
        //{
        //    isMoving = true;
        //}

        if(isMoving)
        {
            m_targetVelocity = move * maxSpeed;
        }

       // m_animator.SetBool("is_moving", isMoving);
        m_animator.SetFloat("run_speed", move.x);

        /******** Jump *********/
        if (Input.GetButtonDown("Jump") && m_grounded)
        {
            // add velocity on the y-axis
            m_velocity.y = jumpTakeOffSpeed;
        }

        else if (Input.GetButtonUp("Jump"))
        {
            if (m_velocity.y > 0.0f)
            {
                // reduce the velocity by half
                m_velocity.y = m_velocity.y * 0.5f;
            }
        }

        //Debug.Log(m_velocity.y);
        m_animator.SetFloat("jump_speed", m_velocity.y);

    }


}
