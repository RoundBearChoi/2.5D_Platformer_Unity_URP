using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/Jump")]
    public class Jump : CharacterAbility
    {
        static bool debug = false;

        public int JumpIndex;
        [Range(0f, 1f)]
        public float JumpTiming;
        public float JumpForce;
        public bool ClearPreviousVelocity;
        [Header("Extra Gravity")]
        public bool CancelPull;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!characterState.JUMP_DATA.DicJumped.ContainsKey(JumpIndex))
            {
                characterState.JUMP_DATA.DicJumped.Add(JumpIndex, false);
            }

            characterState.VERTICAL_VELOCITY_DATA.NoJumpCancel = CancelPull;

            if (JumpTiming == 0f)
            {
                MakeJump(characterState.characterControl);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!characterState.JUMP_DATA.DicJumped[JumpIndex] && stateInfo.normalizedTime >= JumpTiming)
            {
                MakeJump(characterState.characterControl);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.characterControl.JUMP_DATA.DicJumped[JumpIndex] = false;
        }

        void MakeJump(CharacterControl control)
        {
            if (debug)
            {
                Debug.Log("Making jump: " + this.name);
            }

            if (control.JUMP_DATA.DicJumped[JumpIndex])
            {
                Debug.Log("Preventing double jump");
                return;
            }

            if (ClearPreviousVelocity)
            {
                control.RIGID_BODY.velocity = Vector3.zero;
            }

            // automatically turn gravity on before jumping
            if (!control.RIGID_BODY.useGravity)
            {
                control.RIGID_BODY.useGravity = true;
            }

            control.RIGID_BODY.AddForce(Vector3.up * JumpForce);
            control.JUMP_DATA.DicJumped[JumpIndex] = true;
        }
    }
}