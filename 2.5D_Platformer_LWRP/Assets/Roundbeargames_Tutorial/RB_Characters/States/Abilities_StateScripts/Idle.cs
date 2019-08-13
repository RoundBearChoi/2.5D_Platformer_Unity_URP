using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/Idle")]
    public class Idle : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameter.Jump.ToString(), false);
            animator.SetBool(TransitionParameter.Attack.ToString(), false);
            animator.SetBool(TransitionParameter.Move.ToString(), false);

            //CharacterControl control = characterState.GetCharacterControl(animator);
            characterState.characterControl.animationProgress.disallowEarlyTurn = false;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //CharacterControl control = characterState.GetCharacterControl(animator);

            characterState.characterControl.animationProgress.LockDirectionNextState = false;

            if (characterState.characterControl.animationProgress.AttackTriggered)
            {
                animator.SetBool(TransitionParameter.Attack.ToString(), true);
            }

            if (characterState.characterControl.Jump)
            {
                if (!characterState.characterControl.animationProgress.Jumped)
                {
                    animator.SetBool(TransitionParameter.Jump.ToString(), true);
                }
            }
            else
            {
                characterState.characterControl.animationProgress.Jumped = false;
            }

            if (characterState.characterControl.MoveLeft && characterState.characterControl.MoveRight)
            {
                //do nothing
            }
            else if (characterState.characterControl.MoveRight)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), true);
            }
            else if (characterState.characterControl.MoveLeft)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), true);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}