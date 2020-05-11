using Roundbeargames.Datasets;
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
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Jump], false);
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Attack], false);
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], false);

            characterState.characterControl.animationProgress.disallowEarlyTurn = false;
            characterState.BLOCKING_DATA.ClearFrontBlockingObjDic();
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.characterControl.animationProgress.LockDirectionNextState = false;

            if (characterState.characterControl.Jump)
            {
                bool jumped = characterState.characterControl.AIR_CONTROL.GetBool((int)AirControlBool.JUMPED);

                if (!jumped)
                {
                    if (characterState.characterControl.animationProgress.Ground != null)
                    {
                        animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Jump], true);
                    }
                }
            }
            else
            {
                if (!characterState.characterControl.animationProgress.
                    IsRunning(typeof(Jump)))
                {
                    characterState.characterControl.AIR_CONTROL.SetBool((int)AirControlBool.JUMPED, false);
                }
            }

            if (characterState.characterControl.MoveLeft && characterState.characterControl.MoveRight)
            {
                //do nothing
            }
            else if (characterState.characterControl.MoveRight)
            {
                animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], true);
            }
            else if (characterState.characterControl.MoveLeft)
            {
                animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], true);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}