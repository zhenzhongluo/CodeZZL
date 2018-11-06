using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class ProjectileBullet : MonoBehaviour
    {
        public ProjectileBulletStats stats;

        private Rigidbody m_rigidBody;

        void Start()
        {       
        }

        public void OnLaunch(Transform target, float launchAngleInDegree, float ratio)
        {
            m_rigidBody = GetComponent<Rigidbody>();

            Vector3 myXZPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
            Vector3 targetXZPos = new Vector3(target.position.x, 0.0f, target.position.z);

            float gravity = Physics.gravity.y;

            float angleInRadian = launchAngleInDegree * Mathf.Deg2Rad;

            float tanAlpha = Mathf.Tan(angleInRadian);

            // final position - initial position
            float distX = Vector3.Distance(targetXZPos, myXZPos);

            float distY = target.position.y - transform.position.y;

            // Calculate the initial speed required to land the projectile on the target object
            float vZ = Mathf.Sqrt(gravity * distX * distX / (2.0f * (distY - distX * tanAlpha)));
            float vY = tanAlpha * vZ;

            // create the velocity vector in local space and get it in global space
            Vector3 localVelocity = new Vector3(0.0f, vY, vZ);
            Vector3 globalVelocity = transform.TransformDirection(localVelocity);

            if (!float.IsNaN(globalVelocity.x) &&
               !float.IsNaN(globalVelocity.y) &&
               !float.IsNaN(globalVelocity.z))
            {
                float result = stats.spreadRate + ratio;

                m_rigidBody.velocity = globalVelocity * result;
            }
        }
    }

}
