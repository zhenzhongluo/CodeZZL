using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Base class for every physics child class 
*/
public class PhysicsObject : MonoBehaviour
{
    public float minGroundNormalY = 0.65f;
    public float gravityModifier = 1.0f;

    protected bool m_grounded;
    protected Vector2 m_groundNormal;

    protected Vector2 m_velocity;

    protected Vector2 m_targetVelocity;

    protected Rigidbody2D m_rigidbody2D;

    protected ContactFilter2D m_contactFilter;

    protected RaycastHit2D[] m_hitbuffer = new RaycastHit2D[16];

    protected List<RaycastHit2D> m_hitBufferList = new List<RaycastHit2D>(16);

    protected const float m_minMoveDistance = 0.001f;

    // This is a padding to be added it to the distance to say our collider
    // cannot pass inside another collider
    protected const float m_shellRadius = 0.01f;

    protected float m_deltaTime = 0.0f;

   // [HideInInspector]
    public bool isMoving;

   // [HideInInspector]
    public bool facingRight;

    protected bool m_flag;

    void OnEnable()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();    
    }

    // Use this for initialization
    void Start ()
    {
        m_flag = facingRight;

        m_contactFilter.useTriggers = false;

        // This is to say please just use the settings
        // from the Physics2D settings to determine
        // what layers are going to check against.
        m_contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));

        m_contactFilter.useLayerMask = true;
	}

    // Update is called once per frame
    void Update ()
    {
        // reset
        m_targetVelocity = Vector2.zero;

        // Reset
        if(!isMoving)
        {
            m_deltaTime = 0.0f;
        }
        else if (m_flag != facingRight)
        {
            m_deltaTime = 0.0f;
            m_flag = facingRight;
        }

        // keep the time rate limited
        if(m_deltaTime >= 1.0f)
        {
            m_deltaTime = 1.0f;
        }
        else if(m_deltaTime <= -1.0f)
        {
            m_deltaTime = -1.0f;
        }

        // normal situation
        m_deltaTime += Time.deltaTime;

        
        ComputeVelocity();

        OnRotate();
        OnJump();
    }

    protected virtual void ComputeVelocity()
    {

    }

    // For AI
    protected virtual void OnRotate()
    {

    }

    // For AI
    protected virtual void OnJump()
    {

    }

    void FixedUpdate()
    {
        OnMove();

    }

    void OnMove()
    {
        m_velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;

        m_velocity.x = m_targetVelocity.x;

        m_grounded = false;

        Vector2 deltaPos = m_velocity * Time.deltaTime;

        // This direction will be a line that is perpendicular to the ground normal
        Vector2 moveAlongGroundDir = new Vector2(m_groundNormal.y, -m_groundNormal.x);

        // Calculate the x position (horizontal movement)
        Vector2 move = moveAlongGroundDir * deltaPos.x;

        bool yMovement = false;
        Movement(move, yMovement);

        // Calculate the y position (vertical movement)
        move = Vector2.up * deltaPos.y;

        yMovement = true;
        Movement(move, yMovement);
    }

    /*
        Idea:
        1. Separate movement calculation
        2. First to calculate the movement on the x-axis.
           Do this first because it's easier to handle slopes
        3. Then to calculate the movement on the y-axis
    */
    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if(distance > m_minMoveDistance)
        {
            SetRaycastHitResults(move, distance);
            
            // Now we are checking each raycast2D hit object's normal
            // to determine the angles of the thing we are colliding with.
            for(int i = 0;i < m_hitBufferList.Count; i++)
            {
                Vector2 currentNormal = m_hitBufferList[i].normal;

                // Check if the player is grounded.
                if(currentNormal.y > minGroundNormalY)
                {
                    // This check will not allow players to slide down when
                    // there are any slopes on the scene, because the ground 
                    // is determined by the angle between the normal of the 
                    // obstacle and the player's normal.
                    // If the angle is within a certain angle value, then this
                    // obstacle will be considered as a ground.
                    m_grounded = true;

                    // check the vertical movement
                    if(yMovement)
                    {
                        m_groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(m_velocity, currentNormal);

                if(projection < 0)
                {
                    // Cancel out the part of the velocity taht would be stopped
                    // by the collision.
                    m_velocity = m_velocity - projection * currentNormal;
                }

                // Prevent our collider to be stuck inside other colliders
                float modifiedDistance = m_hitBufferList[i].distance - m_shellRadius;
                distance = (modifiedDistance < distance) ? (modifiedDistance) : (distance);
            }
        }

        m_rigidbody2D.position += move.normalized * distance;
    }

    private void SetRaycastHitResults(Vector2 move, float distance)
    {
        int count = m_rigidbody2D.Cast(move, m_contactFilter, m_hitbuffer, distance + m_shellRadius);

        // We are going to copy only the indecies from the 
        // hit buffer array that actually hit something
        m_hitBufferList.Clear();

        for (int i = 0; i < count; i++)
        {
            m_hitBufferList.Add(m_hitbuffer[i]);
        }
    }
}
