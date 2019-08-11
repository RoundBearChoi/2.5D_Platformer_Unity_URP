using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/UpdateBoxCollider")]
    public class UpdateBoxCollider : StateData
    {
        public Vector3 TargetCenter;
        public float CenterUpdateSpeed;
        [Space(10)]
        public Vector3 TargetSize;
        public float SizeUpdateSpeed;
        [Space(10)]
        public bool KeepUpdating;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //CharacterControl control = characterState.GetCharacterControl(animator);
            characterState.characterControl.animationProgress.UpdatingBoxCollider = true;

            characterState.characterControl.animationProgress.TargetSize = TargetSize;
            characterState.characterControl.animationProgress.Size_Speed = SizeUpdateSpeed;

            characterState.characterControl.animationProgress.TargetCenter = TargetCenter;
            characterState.characterControl.animationProgress.Center_Speed = CenterUpdateSpeed;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //CharacterControl control = characterState.GetCharacterControl(animator);

            if (!KeepUpdating)
            {
                characterState.characterControl.animationProgress.UpdatingBoxCollider = false;
            }
        }
    }
}