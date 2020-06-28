using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/InstantTransition")]
    public class InstantTransition : StateData
    {
        public Instant_Transition_States TransitionTo;
        public List<TransitionConditionType> transitionConditions = new List<TransitionConditionType>();
        public float CrossFade;
        public float Offset;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (TransitionConditionChecker.MakeTransition(characterState.characterControl, transitionConditions))
            {
                if (!characterState.characterControl.animationProgress.LockTransition &&
                    !characterState.ANIMATION_DATA.InstantTransitionMade)
                {
                    characterState.ANIMATION_DATA.InstantTransitionMade = true;
                    MakeInstantTransition(characterState.characterControl);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.ANIMATION_DATA.InstantTransitionMade = false;
        }

        void MakeInstantTransition(CharacterControl control)
        {
            AnimatorStateInfo nextInfo = control.SkinnedMeshAnimator.GetNextAnimatorStateInfo(0);

            if (!control.ANIMATION_DATA.IsRunning(typeof(Attack)))
            {
                if (nextInfo.shortNameHash != HashManager.Instance.DicInstantTransitionStates[TransitionTo])
                {
                    if (CrossFade <= 0f)
                    {
                        control.SkinnedMeshAnimator.Play(
                            HashManager.Instance.DicInstantTransitionStates[TransitionTo], 0);
                    }
                    else
                    {
                        if (Offset <= 0f)
                        {
                            control.SkinnedMeshAnimator.CrossFade(
                                HashManager.Instance.DicInstantTransitionStates[TransitionTo],
                                CrossFade, 0);
                        }
                        else
                        {
                            control.SkinnedMeshAnimator.CrossFade(
                                HashManager.Instance.DicInstantTransitionStates[TransitionTo],
                                CrossFade, 0, Offset);
                        }
                    }
                }
            }
        }
    }
}