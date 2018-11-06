using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "PluggableAI/AI/Decisions/Look")]
    public class LookDecision : AI.Decision
    {
        public override bool Decide(AI.StateController controller)
        {
            //Debug.Log("Look Decision!!");
            return Look(controller);
        }

        private bool Look(AI.StateController controller)
        {
            Debug.DrawRay(controller.eyes.position,
                          controller.eyes.forward.normalized *
                          controller.aiController.enemyStats.lookRange,
                          Color.green);

            RaycastHit2D hit;

            hit = Physics2D.CircleCast(controller.eyes.position,
                                    controller.aiController.enemyStats.lookSphereCastRadius,
                                    controller.eyes.forward,
                                    controller.aiController.enemyStats.lookRange,
                                    1 << LayerMask.NameToLayer("PlayerLayer"));

            if (hit)
            {
                controller.chaseTarget = hit.transform;
                return true;
            }
            else
            {
                //controller.chaseTarget = null;
                return false;
            }

            //if (Physics.CircleCast(controller.eyes.position,
            //                      controller.aiController.enemyStats.lookSphereCastRadius,
            //                      controller.eyes.forward,
            //                      out hit,
            //                      controller.aiController.enemyStats.lookRange) &&
            //                      hit.collider.CompareTag("Player"))
            //{
            //    controller.chaseTarget = hit.transform;
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
    }
}
