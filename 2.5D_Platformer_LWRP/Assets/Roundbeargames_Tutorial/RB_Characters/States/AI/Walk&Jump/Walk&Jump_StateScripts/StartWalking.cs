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
            //CharacterControl control = characterState.GetCharacterControl(animator);

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
            //CharacterControl control = characterState.GetCharacterControl(animator);
            Vector3 dist = characterState.characterControl.aiProgress.pathfindingAgent.StartSphere.transform.position 
                - characterState.characterControl.transform.position;

            //jump
            if (characterState.characterControl.aiProgress.pathfindingAgent.StartSphere.transform.position.y
                < characterState.characterControl.aiProgress.pathfindingAgent.EndSphere.transform.position.y)
            {
                if (Vector3.SqrMagnitude(dist) < 0.01f)
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
                if (Vector3.SqrMagnitude(dist) < 0.5f)
                {
                    characterState.characterControl.MoveRight = false;
                    characterState.characterControl.MoveLeft = false;

                    Vector3 playerDist = characterState.characterControl.transform.position - CharacterManager.Instance.GetPlayableCharacter().transform.position;
                    if (playerDist.sqrMagnitude > 1f)
                    {
                        animator.gameObject.SetActive(false);
                        animator.gameObject.SetActive(true);
                    }

                    //temporary attack solution
                    /*else
                    {
                        if (CharacterManager.Instance.GetPlayableCharacter().damageDetector.DamageTaken == 0)
                        {
                            if (control.IsFacingForward())
                            {
                                control.MoveRight = true;
                                control.MoveLeft = false;
                                control.Attack = true;
                            }
                            else
                            {
                                control.MoveRight = false;
                                control.MoveLeft = true;
                                control.Attack = true;
                            }
                        }
                    }*/
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