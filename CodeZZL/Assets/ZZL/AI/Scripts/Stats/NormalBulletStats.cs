using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "PluggableAI/AI/Stats/NormalBulletStats")]
    public class NormalBulletStats : ScriptableObject
    {
        public float damage = 20.0f;

        public float speed = 15.0f;
    }
}
