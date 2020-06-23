using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/InstantTransition")]
    public class InstantTransition : StateData
    {
        public Animation_States TransitionTo;
        public List<TransitionConditionType> transitionConditions = new List<TransitionConditionType>();

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (TransitionConditionChecker.MakeTransition(characterState.characterControl, transitionConditions))
            {
                if (!characterState.characterControl.animationProgress.LockTransition)
                {
                    characterState.characterControl.SkinnedMeshAnimator.Play(
                        HashManager.Instance.DicAnimationStates[TransitionTo], 0);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}