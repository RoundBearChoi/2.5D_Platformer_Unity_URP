using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AI/AITransitionCondition")]
    public class AITransitionCondition : StateData
    {
        public enum AITransitionType
        {
            RUN_TO_WALK,
            WALK_TO_RUN,
        }

        public AITransitionType aiTransition;
        public AI_TYPE NextAI;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //CharacterControl control = characterState.GetCharacterControl(animator);
            if (TransitionToNextAI(characterState.characterControl))
            {
                characterState.characterControl.aiController.TriggerAI(NextAI);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        bool TransitionToNextAI(CharacterControl control)
        {
            if (aiTransition == AITransitionType.RUN_TO_WALK)
            {
                Vector3 dist = control.aiProgress.pathfindingAgent.StartSphere.transform.position - control.transform.position;

                if (Vector3.SqrMagnitude(dist) < 2f)
                {
                    return true;
                }
            }
            else if (aiTransition == AITransitionType.WALK_TO_RUN)
            {
                Vector3 dist = control.aiProgress.pathfindingAgent.StartSphere.transform.position - control.transform.position;

                if (Vector3.SqrMagnitude(dist) > 2f)
                {
                    return true;
                }
            }

            return false;
        }
    }
}