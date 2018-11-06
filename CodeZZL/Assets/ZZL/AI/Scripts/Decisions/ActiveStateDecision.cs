using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "PluggableAI/AI/Decisions/ActiveState")]
    public class ActiveStateDecision : AI.Decision
    {
        public override bool Decide(AI.StateController controller)
        {
            Debug.Log("Active State Decision !!!");
            bool chaseTargetIsActive = controller.chaseTarget.gameObject.activeSelf;

            return chaseTargetIsActive;
        }
    }
}
