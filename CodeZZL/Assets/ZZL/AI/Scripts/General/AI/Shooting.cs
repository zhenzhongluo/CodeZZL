using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Shooting : MonoBehaviour
    {
        public GameObject spawnPoint;

        //public float fireTime = 1f;

        [HideInInspector]
        public StateController stateController;

        void Start()
        {
            stateController = GetComponent<StateController>();

            //InvokeRepeating("Fire", fireTime, fireTime);
        }

        void Update()
        {
            RotateSpawnPoint();    
        }

        public void Fire()
        {
            GameObject obj = ObjectPoolerScript.current.GetNormalBullet();

            if (!obj)
            {
                return;
            }

            obj.transform.position = spawnPoint.transform.position;
            obj.transform.rotation = spawnPoint.transform.rotation;
            obj.SetActive(true);
        }

        // Rotate the spawn point at the Z-axis to face the player
        private void RotateSpawnPoint()
        {
            Transform target = stateController.chaseTarget;

            if(target)
            {
                spawnPoint.transform.LookAt(target.position);
            }
        }

    }

}
