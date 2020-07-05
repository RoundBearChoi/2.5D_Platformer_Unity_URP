using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/WallSlide")]
    public class WallSlide : CharacterAbility
    {
        public Vector3 MaxFallVelocity;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.characterControl.MoveLeft = false;
            characterState.characterControl.MoveRight = false;

            characterState.MOMENTUM_DATA.Momentum = 0f;
            characterState.JUMP_DATA.CanWallJump = false;
            characterState.VERTICAL_VELOCITY_DATA.MaxWallSlideVelocity = MaxFallVelocity;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!characterState.characterControl.Jump)
            {
                characterState.JUMP_DATA.CanWallJump = true;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.VERTICAL_VELOCITY_DATA.MaxWallSlideVelocity = Vector3.zero;
        }
    }
}