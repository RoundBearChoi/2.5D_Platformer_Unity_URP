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

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (ShakeTiming == 0f)
            {
                CameraManager.Instance.ShakeCamera(0.2f);

                //CharacterControl control = characterState.GetCharacterControl(animator);
                characterState.characterControl.animationProgress.CameraShaken = true;
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //CharacterControl control = characterState.GetCharacterControl(animator);

            if (!characterState.characterControl.animationProgress.CameraShaken)
            {
                if (stateInfo.normalizedTime >= ShakeTiming)
                {
                    characterState.characterControl.animationProgress.CameraShaken = true;
                    CameraManager.Instance.ShakeCamera(0.2f);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //CharacterControl control = characterState.GetCharacterControl(animator);
            characterState.characterControl.animationProgress.CameraShaken = false;
        }
    }
}