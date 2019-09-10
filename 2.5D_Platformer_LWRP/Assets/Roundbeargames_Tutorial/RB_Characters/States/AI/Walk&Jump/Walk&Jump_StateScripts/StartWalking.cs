using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AI/StartWalking")]
    public class StartWalking : StateData
    {
        public Vector3 TargetDir = new Vector3();

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            WalkStraightTowardsTarget(characterState.characterControl);
        }

        public void WalkStraightTowardsTarget(CharacterControl control)
        {
            TargetDir = control.aiProgress.pathfindingAgent.StartSphere.transform.position - control.transform.position;

            if (TargetDir.z > 0f)
            {
                control.MoveRight = true;
                control.MoveLeft = false;
            }
            else
            {
                control.MoveRight = false;
                control.MoveLeft = true;
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //jump
            if (characterState.characterControl.aiProgress.pathfindingAgent.StartSphere.transform.position.y
                < characterState.characterControl.aiProgress.pathfindingAgent.EndSphere.transform.position.y)
            {
                if (characterState.characterControl.aiProgress.GetDistanceToDestination() < 0.01f)
                {
                    characterState.characterControl.MoveRight = false;
                    characterState.characterControl.MoveLeft = false;

                    animator.SetBool(AI_Walk_Transitions.jump_platform.ToString(), true);
                }
            }

            //fall
            if (characterState.characterControl.aiProgress.pathfindingAgent.StartSphere.transform.position.y
                > characterState.characterControl.aiProgress.pathfindingAgent.EndSphere.transform.position.y)
            {
                animator.SetBool(AI_Walk_Transitions.fall_platform.ToString(), true);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(AI_Walk_Transitions.jump_platform.ToString(), false);
            animator.SetBool(AI_Walk_Transitions.fall_platform.ToString(), false);
        }
    }
}