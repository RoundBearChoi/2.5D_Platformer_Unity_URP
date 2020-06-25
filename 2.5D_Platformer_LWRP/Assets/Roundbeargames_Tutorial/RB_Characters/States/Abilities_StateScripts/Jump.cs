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
        public bool CancelPull;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.JUMP_DATA.Jumped = false;
            characterState.VERTICAL_VELOCITY_DATA.NoJumpCancel = CancelPull;

            if (JumpTiming == 0f)
            {
                MakeJump(characterState.characterControl);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!characterState.JUMP_DATA.Jumped && stateInfo.normalizedTime >= JumpTiming)
            {
                MakeJump(characterState.characterControl);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        void MakeJump(CharacterControl control)
        {
            // automatically turn gravity on before jumping
            if (!control.RIGID_BODY.useGravity)
            {
                control.RIGID_BODY.useGravity = true;
            }

            control.RIGID_BODY.AddForce(Vector3.up * JumpForce);
            control.JUMP_DATA.Jumped = true;
        }
    }
}