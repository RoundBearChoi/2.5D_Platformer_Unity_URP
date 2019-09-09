using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AI/StartWalking")]
    public class StartWalking : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            Vector3 dir = characterState.characterControl.aiProgress.pathfindingAgent.StartSphere.transform.position 
                - characterState.characterControl.transform.position;

            if (dir.z > 0f)
            {
                characterState.characterControl.MoveRight = true;
                characterState.characterControl.MoveLeft = false;
            }
            else
            {
                characterState.characterControl.MoveRight = false;
                characterState.characterControl.MoveLeft = true;
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

            //straight
            if (characterState.characterControl.aiProgress.pathfindingAgent.StartSphere.transform.position.y
                == characterState.characterControl.aiProgress.pathfindingAgent.EndSphere.transform.position.y)
            {
                if (characterState.characterControl.aiProgress.GetDistanceToDestination() < 0.5f)
                {
                    characterState.characterControl.MoveRight = false;
                    characterState.characterControl.MoveLeft = false;

                    Vector3 playerDist = characterState.characterControl.transform.position - CharacterManager.Instance.GetPlayableCharacter().transform.position;
                    if (playerDist.sqrMagnitude > 1f)
                    {
                        animator.gameObject.SetActive(false);
                        animator.gameObject.SetActive(true);
                    }
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(AI_Walk_Transitions.jump_platform.ToString(), false);
            animator.SetBool(AI_Walk_Transitions.fall_platform.ToString(), false);
        }
    }
}