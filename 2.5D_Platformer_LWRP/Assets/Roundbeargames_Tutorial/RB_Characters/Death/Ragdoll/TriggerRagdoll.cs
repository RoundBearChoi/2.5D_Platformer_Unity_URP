using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/Death/TriggerRagdoll")]
    public class TriggerRagdoll : StateData
    {
        public float TriggerTiming;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //CharacterControl control = characterState.GetCharacterControl(animator);

            if (stateInfo.normalizedTime >= TriggerTiming)
            {
                if (!characterState.characterControl.animationProgress.RagdollTriggered)
                {
                    if (characterState.characterControl.SkinnedMeshAnimator.enabled)
                    {
                        characterState.characterControl.animationProgress.RagdollTriggered = true;
                    }
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //CharacterControl control = characterState.GetCharacterControl(animator);
            //control.animationProgress.RagdollTriggered = false;
        }
    }
}