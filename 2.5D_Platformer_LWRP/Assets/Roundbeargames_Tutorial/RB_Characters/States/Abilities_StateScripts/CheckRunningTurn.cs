using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/CheckRunningTurn")]
    public class CheckRunningTurn : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //CharacterControl control = characterState.GetCharacterControl(animator);

            if (characterState.characterControl.IsFacingForward())
            {
                if (characterState.characterControl.MoveLeft)
                {
                    animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Turn], true);
                }
            }

            if (!characterState.characterControl.IsFacingForward())
            {
                if (characterState.characterControl.MoveRight)
                {
                    animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Turn], true);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Turn], false);
        }
    }
}