using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    /*
        Generic Object Pooler base class for 
        accepting any kinds of game object prefabs 
    */
    public class ObjectPoolerScript : MonoBehaviour
    {
        public static ObjectPoolerScript current;

        public GameObject normalBullet;
        public GameObject projectileBullet;

        public int pooledAmount = 20;
        public bool willGrow = true;

        List<GameObject> pooledObjects;

        private void Awake()
        {
            current = this;
        }

        void Start()
        {
            pooledObjects = new List<GameObject>();

            for (int i = 0; i < pooledAmount; i++)
            {
                GameObject obj1 = (GameObject)Instantiate(normalBullet);
                obj1.SetActive(false);
                pooledObjects.Add(obj1);

                GameObject obj2 = (GameObject)Instantiate(projectileBullet);
                obj2.SetActive(false);
                pooledObjects.Add(obj2);
            }
        }

        public GameObject GetNormalBullet()
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == "NormalBullet")
                {
                    return pooledObjects[i];
                }
            }

            if (willGrow)
            {
                GameObject obj = (GameObject)Instantiate(normalBullet);

                pooledObjects.Add(obj);
                return obj;
            }

            return null;
        }

        public GameObject GetProjectileBullet()
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == "ProjectileBullet")
                {
                    return pooledObjects[i];
                }
            }

            if (willGrow)
            {
                GameObject obj = (GameObject)Instantiate(projectileBullet);

                pooledObjects.Add(obj);
                return obj;
            }

            return null;
        }
    }

}