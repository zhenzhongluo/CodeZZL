using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "PluggableAI/AI/Actions/Launch")]
    public class LaunchAction : AI.Action
    {
        public override void Act(AI.StateController controller)
        {
            Launch(controller);
        }

        private void Launch(AI.StateController controller)
        {
            Debug.DrawRay(controller.eyes.position,
                            controller.eyes.forward.normalized *
                            controller.aiController.enemyStats.attackRange,
                            Color.red);

            RaycastHit2D hit;

            hit = Physics2D.CircleCast(controller.eyes.position,
                                    controller.aiController.enemyStats.lookSphereCastRadius,
                                    controller.eyes.forward,
                                    controller.aiController.enemyStats.projectileRange,
                                    1 << LayerMask.NameToLayer("PlayerLayer"));

            if (hit)
            {
                if (controller.CheckIfCountDownElapsed(controller.aiController.enemyStats.projectileRate))
                {
                    //float launchAngleInDegree = controller.aiController.enemyStats.launchAngleInDegree;
                    //controller.projectile.Fire(hit.transform, launchAngleInDegree);

                    controller.projectile.Fire();
                    controller.projectileBulletsPerShot--;

                    // reset
                    if (controller.projectileBulletsPerShot == 0)
                    {
                        controller.OnExitState();
                        controller.projectileBulletsPerShot = controller.aiController.enemyStats.projectileBulletsPerShot;
                        controller.projectileSpreadOffset = 0.0f;
                    }
                }
            }


            //if (Physics.SphereCast(controller.eyes.position,
            //                      controller.aiController.enemyStats.lookSphereCastRadius,
            //                      controller.eyes.forward,
            //                      out hit,
            //                      controller.aiController.enemyStats.projectileRange) &&
            //                      hit.collider.CompareTag("Player"))
            //{
            //    if (controller.CheckIfCountDownElapsed(controller.aiController.enemyStats.projectileRate))
            //    {
            //        //float launchAngleInDegree = controller.aiController.enemyStats.launchAngleInDegree;
            //        //controller.projectile.Fire(hit.transform, launchAngleInDegree);

            //        controller.projectile.Fire();
            //        controller.projectileBulletsPerShot--;

            //        // reset
            //        if (controller.projectileBulletsPerShot == 0)
            //        {
            //            controller.OnExitState();
            //            controller.projectileBulletsPerShot = controller.aiController.enemyStats.projectileBulletsPerShot;
            //            controller.projectileSpreadOffset = 0.0f;
            //        }
            //    }
            //}
        }
    }

}
