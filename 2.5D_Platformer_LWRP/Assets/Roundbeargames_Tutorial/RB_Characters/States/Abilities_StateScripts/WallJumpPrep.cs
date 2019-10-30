using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/WallJumpPrep")]
    public class WallJumpPrep : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.characterControl.MoveLeft = false;
            characterState.characterControl.MoveRight = false;
            characterState.characterControl.animationProgress.AirMomentum = 0f;

            characterState.characterControl.RIGID_BODY.velocity = Vector3.zero;

            if (characterState.characterControl.IsFacingForward())
            {
                characterState.characterControl.FaceForward(false);
            }
            else
            {
                characterState.characterControl.FaceForward(true);
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