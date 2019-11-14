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

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!characterState.characterControl.RIGID_BODY.useGravity)
            {
                characterState.characterControl.RIGID_BODY.MovePosition(
                    characterState.characterControl.ledgeChecker.GrabbedLedge.transform.position +
                    characterState.characterControl.ledgeChecker.GrabbedLedge.Offset);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}