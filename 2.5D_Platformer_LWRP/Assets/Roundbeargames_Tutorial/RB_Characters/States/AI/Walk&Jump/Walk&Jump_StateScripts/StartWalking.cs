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
            characterState.characterControl.aiProgress.SetRandomFlyingKick();
            characterState.characterControl.aiController.WalkStraightToStartSphere();
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.characterControl.Attack)
            {
                return;
            }

            //jump
            if (characterState.characterControl.aiProgress.EndSphereIsHigher())
            {
                if (characterState.characterControl.aiProgress.AIDistanceToStartSphere() < 0.08f)
                {
                    characterState.characterControl.MoveRight = false;
                    characterState.characterControl.MoveLeft = false;

                    animator.SetBool(HashManager.Instance.
                        DicAITrans[AI_Walk_Transitions.jump_platform], true);
                    return;
                }
            }

            //fall
            if (characterState.characterControl.aiProgress.EndSphereIsLower())
            {
                characterState.characterControl.aiController.WalkStraightToEndSphere();

                animator.SetBool(HashManager.Instance.
                    DicAITrans[AI_Walk_Transitions.fall_platform], true);
                return;
            }

            //straight
            if (characterState.characterControl.aiProgress.AIDistanceToStartSphere() > 1.5f)
            {
                characterState.characterControl.Turbo = true;
            }
            else
            {
                characterState.characterControl.Turbo = false;
            }

            characterState.characterControl.aiController.WalkStraightToStartSphere();

            if (characterState.characterControl.aiProgress.AIDistanceToEndSphere() < 1f)
            {
                characterState.characterControl.Turbo = false;
                characterState.characterControl.MoveRight = false;
                characterState.characterControl.MoveLeft = false;
            }

            if (characterState.characterControl.aiProgress.TargetIsOnSamePlatform())
            {
                characterState.characterControl.aiProgress.RepositionDestination();
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(HashManager.Instance.
                DicAITrans[AI_Walk_Transitions.jump_platform], false);

            animator.SetBool(HashManager.Instance.
                DicAITrans[AI_Walk_Transitions.fall_platform], false);
        }
    }
}