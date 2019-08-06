using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/CheckTurbo")]
    public class CheckTurbo : StateData
    {
        public bool MustRequireMovement;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.Turbo)
            {
                if (MustRequireMovement)
                {
                    if (control.MoveLeft || control.MoveRight)
                    {
                        animator.SetBool(TransitionParameter.Turbo.ToString(), true);
                    }
                    else
                    {
                        animator.SetBool(TransitionParameter.Turbo.ToString(), false);
                    }
                }
                else
                {
                    animator.SetBool(TransitionParameter.Turbo.ToString(), true);
                }
            }
            else
            {
                animator.SetBool(TransitionParameter.Turbo.ToString(), false);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}