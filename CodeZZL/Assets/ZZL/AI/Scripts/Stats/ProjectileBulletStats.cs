using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "PluggableAI/AI/Stats/ProjectileBulletStats")]
    public class ProjectileBulletStats : ScriptableObject
    {
        public float damage = 50.0f;

        public float spreadRate = 1.0f;
    }
}
