using Roundbeargames.Datasets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/WallSlide")]
    public class WallSlide : StateData
    {
        public Vector3 MaxFallVelocity;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.characterControl.MoveLeft = false;
            characterState.characterControl.MoveRight = false;

            characterState.MOMENTUM_DATA.Momentum = 0f;
            characterState.characterControl.AIR_CONTROL.SetBool((int)AirControlBool.CAN_WALL_JUMP, false);

            characterState.VERTICAL_VELOCITY_DATA.MaxWallSlideVelocity = MaxFallVelocity;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!characterState.characterControl.Jump)
            {
                characterState.characterControl.AIR_CONTROL.SetBool((int)AirControlBool.CAN_WALL_JUMP, true);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.VERTICAL_VELOCITY_DATA.MaxWallSlideVelocity = Vector3.zero;
        }
    }
}