using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    /*
        This is the state machine class that handles transitions between states 
    */
    public class StateController : MonoBehaviour
    {
        public List<Transform> wayPointList;

        public Transform jumpWaypoint;

        public Transform eyes;

        public AI.State currentState;
        public AI.State remainState;

        [HideInInspector]
        public int wayPointIndex;

        [HideInInspector]
        public AI.AIController aiController;

        [HideInInspector]
        public AI.Shooting shooting;

        [HideInInspector]
        public AI.Projectile projectile;

        [HideInInspector]
        public Transform chaseTarget;

        [HideInInspector]
        public float stateTimeElapsed;

        [HideInInspector]
        public int normalBulletsPerShot;

        [HideInInspector]
        public int projectileBulletsPerShot;
        
        [HideInInspector]
        public float projectileSpreadOffset = 0.0f;

        private bool m_aiActive;

        private float m_lookSphereCastRadius;

        void Awake()
        {
            m_aiActive = true;
        }

        // Use this for initialization
        void Start()
        {
            aiController = GetComponent<AI.AIController>();

            shooting = GetComponent<AI.Shooting>();
            projectile = GetComponent<AI.Projectile>();

            wayPointIndex = 0;

            if(aiController)
            {
                m_lookSphereCastRadius = aiController.enemyStats.lookSphereCastRadius;

                normalBulletsPerShot = aiController.enemyStats.normalBulletsPerShot;

                projectileBulletsPerShot = aiController.enemyStats.projectileBulletsPerShot;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(!m_aiActive)
            {
                return;
            }

            currentState.UpdateState(this);
        }

        // For Debug: allow us to draw things on the scene view 
        // but not in the game view
        void OnDrawGizmos()
        {
            if (currentState && eyes)
            {
                Gizmos.color = currentState.sceneGizmoColor;

                Gizmos.DrawWireSphere(eyes.position, m_lookSphereCastRadius);
            }
        }

        public void TransitionToState(AI.State nextState)
        {
            if (nextState != remainState)
            {
                currentState = nextState;
                OnExitState();
            }
        }

        // when stateTimeElapsed >= duration, start firing
        public bool CheckIfCountDownElapsed(float duration)
        {
            stateTimeElapsed += Time.deltaTime;

            return (stateTimeElapsed >= duration);
        }

        public void OnExitState()
        {
            stateTimeElapsed = 0.0f;
        }
    }

}
