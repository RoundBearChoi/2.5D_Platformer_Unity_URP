using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/CheckAttackPress")]
    public class CheckAttackPress : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.characterControl.Attack)
            {
                characterState.characterControl.SkinnedMeshAnimator.SetBool(
                    HashManager.Instance.DicMainParams[TransitionParameter.Attack], true);
            }
            else
            {
                characterState.characterControl.SkinnedMeshAnimator.SetBool(
                    HashManager.Instance.DicMainParams[TransitionParameter.Attack], false);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}