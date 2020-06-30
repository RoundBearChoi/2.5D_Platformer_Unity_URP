using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/CheckTurbo")]
    public class CheckTurbo : CharacterAbility
    {
        public bool MustRequireMovement;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.characterControl.Turbo)
            {
                if (MustRequireMovement)
                {
                    if (characterState.characterControl.MoveLeft || characterState.characterControl.MoveRight)
                    {
                        animator.SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Turbo], true);
                    }
                    else
                    {
                        animator.SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Turbo], false);
                    }
                }
                else
                {
                    animator.SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Turbo], true);
                }
            }
            else
            {
                animator.SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Turbo], false);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}