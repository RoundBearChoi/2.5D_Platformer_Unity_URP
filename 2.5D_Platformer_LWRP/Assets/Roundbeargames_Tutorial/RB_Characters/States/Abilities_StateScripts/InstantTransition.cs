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

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (TransitionConditionChecker.MakeTransition(characterState.characterControl, transitionConditions))
            {
                if (!characterState.characterControl.animationProgress.LockTransition)
                {
                    AnimatorStateInfo nextInfo = characterState.characterControl.SkinnedMeshAnimator.
                        GetNextAnimatorStateInfo(0);

                    if (!characterState.characterControl.ANIMATION_DATA.IsRunning(typeof(Attack)))
                    {
                        if (nextInfo.shortNameHash != HashManager.Instance.DicInstantTransitionStates[TransitionTo])
                        {
                            if (CrossFade <= 0f)
                            {
                                characterState.characterControl.SkinnedMeshAnimator.Play(
                                    HashManager.Instance.DicInstantTransitionStates[TransitionTo], 0);
                            }
                            else
                            {
                                characterState.characterControl.SkinnedMeshAnimator.CrossFade(
                                    HashManager.Instance.DicInstantTransitionStates[TransitionTo], CrossFade, 0);
                            }
                        }
                    }
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}