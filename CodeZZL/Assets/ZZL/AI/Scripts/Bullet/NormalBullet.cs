using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class NormalBullet : MonoBehaviour
    {
        public NormalBulletStats stats;

        void Update()
        {
            OnMove();
        }

        public void OnMove()
        {
            transform.Translate(0, 0, stats.speed * Time.deltaTime);
        }
    }

}
