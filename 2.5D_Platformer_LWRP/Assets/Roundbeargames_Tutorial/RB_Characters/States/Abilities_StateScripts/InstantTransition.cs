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
        public float CrossFade;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (TransitionConditionChecker.MakeTransition(characterState.characterControl, transitionConditions))
            {
                if (!characterState.characterControl.animationProgress.LockTransition)
                {
                    if (CrossFade <= 0f)
                    {
                        characterState.characterControl.SkinnedMeshAnimator.Play(
                            HashManager.Instance.DicAnimationStates[TransitionTo], 0);
                    }
                    else
                    {
                        characterState.characterControl.SkinnedMeshAnimator.CrossFade(
                            HashManager.Instance.DicAnimationStates[TransitionTo], CrossFade, 0);
                    }
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}