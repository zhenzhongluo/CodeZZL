using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "PluggableAI/AI/Actions/Patrol")]
    public class PatrolAction : AI.Action
    {
        public override void Act(AI.StateController controller)
        {
            //Debug.Log("Patrol!!");
            Patrol(controller);
        }

        private void Patrol(AI.StateController controller)
        {

            Transform target = controller.wayPointList[controller.wayPointIndex];
            //Transform target = controller.chaseTarget;

            Vector3 targetPosition = target.transform.position;
            Vector3 myPosition = controller.transform.position;

            //float distance = 1.0f;

            Vector3 result = targetPosition - myPosition;

            float currentDist = result.magnitude;

            if(currentDist > controller.aiController.enemyStats.lookSphereCastRadius)
            {
                controller.aiController.isMoving = true;
                controller.aiController.targetReached = false;
                controller.aiController.moveType = MoveType.PATROL;
            }
            else
            {
                controller.aiController.isMoving = false;
                controller.aiController.targetReached = true;
                controller.aiController.moveType = MoveType.NONE;

                controller.wayPointIndex = (controller.wayPointIndex + 1) % controller.wayPointList.Count;
            }
        }
    }
}
