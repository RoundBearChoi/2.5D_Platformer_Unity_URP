using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/CheckRunningTurn")]
    public class CheckRunningTurn : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.ROTATION_DATA.LockTurn)
            {
                animator.SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Turn], false);
                return;
            }

            if (characterState.ROTATION_DATA.IsFacingForward())
            {
                if (characterState.characterControl.MoveLeft)
                {
                    animator.SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Turn], true);
                }
            }

            if (!characterState.ROTATION_DATA.IsFacingForward())
            {
                if (characterState.characterControl.MoveRight)
                {
                    animator.SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Turn], true);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Turn], false);
        }
    }
}