using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/ShakeCamera")]
    public class ShakeCamera : StateData
    {
        [Range(0f, 0.99f)]
        public float ShakeTiming;
        private bool isShaken = false;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (ShakeTiming == 0f)
            {
                CameraManager.Instance.ShakeCamera(0.2f);
                isShaken = true;
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!isShaken)
            {
                if (stateInfo.normalizedTime >= ShakeTiming)
                {
                    isShaken = true;
                    CameraManager.Instance.ShakeCamera(0.2f);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            isShaken = false;
        }
    }
}