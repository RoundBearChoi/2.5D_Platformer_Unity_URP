using Roundbeargames.Datasets;
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
            characterState.JUMP_DATA.Jumped = false;

            if (JumpTiming == 0f)
            {
                characterState.characterControl.RIGID_BODY.AddForce(Vector3.up * JumpForce);
                characterState.JUMP_DATA.Jumped = true;
            }

            characterState.VERTICAL_VELOCITY_DATA.NoJumpCancel = CancelPull;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!characterState.JUMP_DATA.Jumped && stateInfo.normalizedTime >= JumpTiming)
            {
                characterState.characterControl.RIGID_BODY.AddForce(Vector3.up * JumpForce);
                characterState.JUMP_DATA.Jumped = true;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}