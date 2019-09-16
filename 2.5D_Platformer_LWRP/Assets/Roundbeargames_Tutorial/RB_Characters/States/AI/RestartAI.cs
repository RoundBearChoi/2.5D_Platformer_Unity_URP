using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AI/RestartAI")]
    public class RestartAI : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            // walking
            if (characterState.characterControl.aiProgress.AIDistanceToEndSphere() < 1f)
            {
                if (characterState.characterControl.aiProgress.TargetDistanceToEndSphere() > 5f)
                {
                    if (characterState.characterControl.aiProgress.TargetIsGrounded())
                    {
                        characterState.characterControl.aiController.InitializeAI();
                    }
                }
            }

            // landing
            if (characterState.characterControl.animationProgress.IsLanding)
            {
                characterState.characterControl.Turbo = false;
                characterState.characterControl.Jump = false;
                characterState.characterControl.MoveUp = false;
                characterState.characterControl.aiController.InitializeAI();
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}