using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Projectile : MonoBehaviour
    {
        public GameObject spawnPoint;

        [HideInInspector]
        public AIController aiController;

        [HideInInspector]
        public StateController stateController;

        void Start()
        {
            aiController = GetComponent<AIController>();
            stateController = GetComponent<StateController>();     
        }

        public void Fire() 
        {
            GameObject obj = ObjectPoolerScript.current.GetProjectileBullet();

            if (!obj)
            {
                return;
            }

            obj.transform.position = spawnPoint.transform.position;
            obj.transform.rotation = spawnPoint.transform.rotation;
            obj.SetActive(true);

            float launchAngleInDegree = aiController.enemyStats.launchAngleInDegree;
            
            Transform target = stateController.chaseTarget;

            stateController.projectileSpreadOffset += aiController.enemyStats.spreadRate;

            ProjectileBullet bullet = obj.GetComponent<ProjectileBullet>();
            bullet.OnLaunch(target, launchAngleInDegree, stateController.projectileSpreadOffset);
        }
    }
}

