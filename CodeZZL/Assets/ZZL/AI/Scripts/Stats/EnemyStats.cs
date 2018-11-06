﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "PluggableAI/AI/Stats/EnemyStats")]
    public class EnemyStats : ScriptableObject
    {
        public float spreadRate = 0.005f;
        public float projectileRange = 8.0f;
        public float projectileRate = 5.0f;

        public float launchAngleInDegree = 45.0f;

        public int normalBulletsPerShot = 3;
        public int projectileBulletsPerShot = 1;

        public float shootingRange = 5.0f;
        public float shootingRate = 2.0f;

        public float rotateSpeed = 120.0f;
        public float jumpSpeed = 1.0f;

        public float moveSpeed = 1.0f;
        public float chaseSpeed = 2.0f;

        public float lookRange = 40.0f;
        public float lookSphereCastRadius = 1.0f;

        public float attackRange = 1.0f;
        public float attackRate = 1.0f;
        public float attackForce = 15.0f;
        public int attackDamage = 50;

        public float searchDuration = 4.0f;
        public float searchingTurnSpeed = 120.0f;
    }
}
