using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "PluggableAI/AI/Actions/Chase")]
    public class ChaseAction : AI.Action
    {
        public override void Act(AI.StateController controller)
        {
            //Debug.Log("Chase!!!");
            Chase(controller);
        }

        private void Chase(AI.StateController controller)
        {
            Transform target = controller.chaseTarget;
            
            Vector3 targetPosition = target.transform.position;
            Vector3 myPosition = controller.transform.position;

            Vector3 result = targetPosition - myPosition;
            float currentDist = result.magnitude;

            // Not calculating the direction of two points for now, 
            // only calculating the x coordinate because the player can be on top of the enemy
            //float currentDist = Mathf.Abs(targetPosition.x - myPosition.x);

            if (currentDist > controller.aiController.enemyStats.lookSphereCastRadius)
            {
                controller.aiController.isMoving = true;
                controller.aiController.targetReached = false;
                controller.aiController.moveType = MoveType.CHASE;
            }
            else
            {
                controller.aiController.isMoving = false;
                controller.aiController.targetReached = true;
                controller.aiController.moveType = MoveType.NONE;
            }
        }
        
    }
}

