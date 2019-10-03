using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/OffsetOnLedge")]
    public class OffsetOnLedge : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            GameObject anim = characterState.characterControl.SkinnedMeshAnimator.gameObject;
            anim.transform.parent = characterState.characterControl.ledgeChecker.GrabbedLedge.transform;
            anim.transform.localPosition = characterState.characterControl.ledgeChecker.GrabbedLedge.Offset;

            float x;
            float y;
            float z;

            if (characterState.characterControl.IsFacingForward())
            {
                x = characterState.characterControl.ledgeChecker.LedgeCalibration.x;
                y = characterState.characterControl.ledgeChecker.LedgeCalibration.y;
                z = characterState.characterControl.ledgeChecker.LedgeCalibration.z;
            }
            else
            {
                x = characterState.characterControl.ledgeChecker.LedgeCalibration.x;
                y = characterState.characterControl.ledgeChecker.LedgeCalibration.y;
                z = -characterState.characterControl.ledgeChecker.LedgeCalibration.z;
            }

            Vector3 calibration;
            calibration.x = x;
            calibration.y = y;
            calibration.z = z;

            anim.transform.localPosition += calibration;
            
            characterState.characterControl.RIGID_BODY.velocity = Vector3.zero;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}