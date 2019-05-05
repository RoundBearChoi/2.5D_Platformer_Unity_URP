using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/Jump")]
    public class Jump : StateData
    {
        [Range(0f, 1f)]
        public float JumpTiming;
        public float JumpForce;
        //public AnimationCurve Gravity;
        public AnimationCurve Pull;
        private bool isJumped;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (JumpTiming == 0f)
            {
                characterState.GetCharacterControl(animator).RIGID_BODY.AddForce(Vector3.up * JumpForce);
                isJumped = true;
            }
            
            //animator.SetBool(TransitionParameter.Grounded.ToString(), false);
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            //control.GravityMultiplier = Gravity.Evaluate(stateInfo.normalizedTime);
            control.PullMultiplier = Pull.Evaluate(stateInfo.normalizedTime);

            if (!isJumped && stateInfo.normalizedTime >= JumpTiming)
            {
                characterState.GetCharacterControl(animator).RIGID_BODY.AddForce(Vector3.up * JumpForce);
                isJumped = true;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            control.PullMultiplier = 0f;
            isJumped = false;
        }
    }
}