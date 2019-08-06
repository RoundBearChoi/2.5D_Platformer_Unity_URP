using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/TeleportOnLedge")]
    public class TeleportOnLedge : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = CharacterManager.Instance.GetCharacter(animator);

            Vector3 endPosition = control.ledgeChecker.GrabbedLedge.transform.position + control.ledgeChecker.GrabbedLedge.EndPosition;

            control.transform.position = endPosition;
            control.SkinnedMeshAnimator.transform.position = endPosition;
            control.SkinnedMeshAnimator.transform.parent = control.transform;
        }
    }
}