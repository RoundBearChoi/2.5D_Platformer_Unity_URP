using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/ResetLocalPosition")]
    public class ResetLocalPosition : StateData
    {
        public bool OnStart;
        public bool OnEnd;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (OnStart)
            {
                //CharacterControl control = characterState.GetCharacterControl(animator);
                characterState.characterControl.SkinnedMeshAnimator.transform.localPosition = Vector3.zero;
                characterState.characterControl.SkinnedMeshAnimator.transform.localRotation = Quaternion.identity;
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (OnEnd)
            {
                //CharacterControl control = characterState.GetCharacterControl(animator);
                characterState.characterControl.SkinnedMeshAnimator.transform.localPosition = Vector3.zero;
                characterState.characterControl.SkinnedMeshAnimator.transform.localRotation = Quaternion.identity;
            }
        }
    }
}