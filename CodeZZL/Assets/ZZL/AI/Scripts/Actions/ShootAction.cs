using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "PluggableAI/AI/Actions/Shoot")]
    public class ShootAction : AI.Action
    {
        public override void Act(AI.StateController controller)
        {
            Shoot(controller);
        }

        private void Shoot(AI.StateController controller)
        {
            Debug.DrawRay(controller.eyes.position,
                            controller.eyes.forward.normalized *
                            controller.aiController.enemyStats.attackRange,
                            Color.red);

            RaycastHit2D hit;

            hit = Physics2D.CircleCast(controller.eyes.position,
                                    controller.aiController.enemyStats.lookSphereCastRadius,
                                    controller.eyes.forward,
                                    controller.aiController.enemyStats.shootingRange,
                                    1 << LayerMask.NameToLayer("PlayerLayer"));

            if(hit)
            {
                //controller.shooting.Fire();

                if (controller.CheckIfCountDownElapsed(controller.aiController.enemyStats.shootingRate))
                {
                    controller.shooting.Fire();

                    controller.normalBulletsPerShot--;

                    // reset
                    if (controller.normalBulletsPerShot == 0)
                    {
                        controller.OnExitState();
                        controller.normalBulletsPerShot = controller.aiController.enemyStats.normalBulletsPerShot;
                    }
                }
            }

            //if (Physics.SphereCast(controller.eyes.position,
            //                      controller.aiController.enemyStats.lookSphereCastRadius,
            //                      controller.eyes.forward,
            //                      out hit,
            //                      controller.aiController.enemyStats.shootingRange) &&
            //                      hit.collider.CompareTag("Player"))
            //{
            //    //controller.shooting.Fire();

            //    if (controller.CheckIfCountDownElapsed(controller.aiController.enemyStats.shootingRate))
            //    {
            //        controller.shooting.Fire();

            //        controller.normalBulletsPerShot--;

            //        // reset
            //        if(controller.normalBulletsPerShot == 0)
            //        {
            //            controller.OnExitState();
            //            controller.normalBulletsPerShot = controller.aiController.enemyStats.normalBulletsPerShot;
            //        }                 
            //    }
            //}
        }
    }

}
