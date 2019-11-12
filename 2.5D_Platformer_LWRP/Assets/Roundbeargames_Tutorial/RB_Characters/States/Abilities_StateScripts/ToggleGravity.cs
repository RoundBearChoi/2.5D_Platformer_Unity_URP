using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/ToggleGravity")]
    public class ToggleGravity : StateData
    {
        public bool On;
        public bool OnStart;
        public bool OnEnd;
        public float CustomTiming;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (OnStart)
            {
                ToggleGrav(characterState.characterControl);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (CustomTiming != 0f)
            {
                if (CustomTiming <= stateInfo.normalizedTime)
                {
                    ToggleGrav(characterState.characterControl);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (OnEnd)
            {
                ToggleGrav(characterState.characterControl);
            }
        }

        private void ToggleGrav(CharacterControl control)
        {
            control.RIGID_BODY.velocity = Vector3.zero;
            control.RIGID_BODY.useGravity = On;
        }
    }
}