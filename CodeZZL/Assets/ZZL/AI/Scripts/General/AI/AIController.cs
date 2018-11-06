using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public enum MoveType
    {
        PATROL,
        CHASE,

        NONE
    };

    public class AIController : PhysicsObject
    {       
        public AI.EnemyStats enemyStats;

        [HideInInspector]
        public bool canJump;

        [HideInInspector]
        public bool targetReached;

        [HideInInspector]
        public bool canRotate;

        [HideInInspector]
        public bool canLaunchMyself;

        [HideInInspector]
        public MoveType moveType { get; set; }

        private Rigidbody2D m_rigidbody;

        private AI.StateController m_stateController;

        private Animator m_animator;

        // Use this for initialization
        void Start()
        {
            m_animator = GetComponent<Animator>();

            m_stateController = GetComponent<StateController>();

            m_rigidbody = GetComponent<Rigidbody2D>();

            isMoving = false;
            facingRight = true;
            canRotate = false;
            canLaunchMyself = false;

            targetReached = false;
        }

        protected override void ComputeVelocity()
        {
            Vector2 move = Vector2.zero;

            float maxSpeed = 0.0f;

            move.x = m_deltaTime * 2.0f;
           // Debug.Log(move.x);

            if (isMoving)
            {
                if (facingRight)
                {
                    if(moveType == MoveType.PATROL)
                    {
                       // Debug.Log("Patrol!!!!");
                        maxSpeed = enemyStats.moveSpeed;

                        //m_rigidbody.AddForce(new Vector3(enemyStats.moveSpeed, 0, 0), ForceMode2D.Impulse);
                    }
                    else if(moveType == MoveType.CHASE)
                    {
                       // Debug.Log("Chase ~~~");
                        maxSpeed = enemyStats.chaseSpeed;

                        //m_rigidbody.AddForce(new Vector3(enemyStats.chaseSpeed, 0, 0), ForceMode2D.Impulse);
                    }
                }
                else
                {
                    if (moveType == MoveType.PATROL)
                    {
                       // Debug.Log("Patrol!!!!");

                        maxSpeed = -enemyStats.moveSpeed;
                        //m_rigidbody.AddForce(new Vector3(-enemyStats.moveSpeed, 0, 0), ForceMode2D.Impulse);
                    }
                    else if (moveType == MoveType.CHASE)
                    {
                       // Debug.Log("Chase ~~~");
                        maxSpeed = -enemyStats.chaseSpeed;
                        //m_rigidbody.AddForce(new Vector3(-enemyStats.chaseSpeed, 0, 0), ForceMode2D.Impulse);
                    }            
                }

                m_targetVelocity = move * maxSpeed;
            }

            // Update animation
            m_animator.SetFloat("walk_speed", Mathf.Abs(maxSpeed));
        }

        protected override void OnRotate()
        {
            if(canRotate)
            {
                transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f) * enemyStats.rotateSpeed, 180);

                if(!targetReached)
                {
                    facingRight = !facingRight;
                }
            }

            else if (targetReached)
            {
                transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f) * enemyStats.rotateSpeed, 180);
                facingRight = !facingRight;

                targetReached = false;
            }
        }

        protected override void OnJump()
        {
            if (canJump)
            {
                Vector3 jumpDirection = Vector3.up * enemyStats.jumpSpeed;

                m_rigidbody.AddForce(jumpDirection, ForceMode2D.Impulse);
            }
            else
            {
                Vector3 jumpDirection = Vector3.down * enemyStats.jumpSpeed;

                m_rigidbody.AddForce(jumpDirection, ForceMode2D.Impulse);
            }
        }
    }
}
