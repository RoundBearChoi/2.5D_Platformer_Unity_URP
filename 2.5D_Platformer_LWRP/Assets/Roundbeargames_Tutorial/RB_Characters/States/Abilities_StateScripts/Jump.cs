using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/Jump")]
    public class Jump : StateData
    {
        [Range(0f, 1f)]
        public float JumpTiming;
        public float JumpForce;
        [Header("Extra Gravity")]
        //public AnimationCurve Pull;
        public bool CancelPull;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.characterControl.animationProgress.Jumped = false;

            if (JumpTiming == 0f)
            {
                characterState.characterControl.RIGID_BODY.AddForce(Vector3.up * JumpForce);
                characterState.characterControl.animationProgress.Jumped = true;
            }

            characterState.characterControl.animationProgress.CancelPull = CancelPull;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //characterState.characterControl.PullMultiplier = Pull.Evaluate(stateInfo.normalizedTime);

            if (!characterState.characterControl.animationProgress.Jumped && stateInfo.normalizedTime >= JumpTiming)
            {
                characterState.characterControl.RIGID_BODY.AddForce(Vector3.up * JumpForce);
                characterState.characterControl.animationProgress.Jumped = true;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //characterState.characterControl.PullMultiplier = 0f;
        }
    }
}