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
            CharacterControl control = characterState.GetCharacterControl(animator);
            GameObject anim = control.SkinnedMeshAnimator.gameObject;
            anim.transform.parent = control.ledgeChecker.GrabbedLedge.transform;
            anim.transform.localPosition = control.ledgeChecker.GrabbedLedge.Offset;

            control.RIGID_BODY.velocity = Vector3.zero;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}