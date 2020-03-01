using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/SwitchAnimator")]
    public class SwitchAnimator : StateData
    {
        public float SwitchTiming;
        public RuntimeAnimatorController TargetAnimator;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.characterControl.RIGID_BODY.useGravity = true;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= SwitchTiming)
            {
                characterState.characterControl.SkinnedMeshAnimator.runtimeAnimatorController = TargetAnimator;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}