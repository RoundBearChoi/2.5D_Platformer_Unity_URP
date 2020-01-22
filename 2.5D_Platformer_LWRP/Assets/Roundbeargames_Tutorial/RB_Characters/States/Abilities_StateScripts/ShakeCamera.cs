using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/ShakeCamera")]
    public class ShakeCamera : StateData
    {
        [Range(0f, 0.99f)]
        public float ShakeTiming;
        public float ShakeLength;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (ShakeTiming == 0f)
            {
                CameraManager.Instance.ShakeCamera(ShakeLength);
                characterState.characterControl.animationProgress.CameraShaken = true;
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!characterState.characterControl.animationProgress.CameraShaken)
            {
                if (stateInfo.normalizedTime >= ShakeTiming)
                {
                    characterState.characterControl.animationProgress.CameraShaken = true;
                    CameraManager.Instance.ShakeCamera(ShakeLength);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.characterControl.animationProgress.CameraShaken = false;
        }
    }
}