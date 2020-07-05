using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/WallJumpPrep")]
    public class WallJumpPrep : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.characterControl.MoveLeft = false;
            characterState.characterControl.MoveRight = false;
            characterState.MOMENTUM_DATA.Momentum = 0f;

            characterState.characterControl.RIGID_BODY.velocity = Vector3.zero;

            if (characterState.ROTATION_DATA.IsFacingForward())
            {
                characterState.ROTATION_DATA.FaceForward(false);
            }
            else
            {
                characterState.ROTATION_DATA.FaceForward(true);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}