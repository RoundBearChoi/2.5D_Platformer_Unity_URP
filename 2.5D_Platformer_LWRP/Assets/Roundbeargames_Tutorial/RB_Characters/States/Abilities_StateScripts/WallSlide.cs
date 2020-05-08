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

            characterState.characterControl.AIR_CONTROL.SetFloat((int)AirControlFloat.AIR_MOMENTUM, 0f);
            characterState.characterControl.AIR_CONTROL.SetVector3((int)AirControlVector3.MAX_FALL_VELOCITY, MaxFallVelocity);
            characterState.characterControl.AIR_CONTROL.SetBool((int)AirControlBool.CAN_WALL_JUMP, false);
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
            characterState.characterControl.AIR_CONTROL.SetVector3((int)AirControlVector3.MAX_FALL_VELOCITY, Vector3.zero);
        }
    }
}