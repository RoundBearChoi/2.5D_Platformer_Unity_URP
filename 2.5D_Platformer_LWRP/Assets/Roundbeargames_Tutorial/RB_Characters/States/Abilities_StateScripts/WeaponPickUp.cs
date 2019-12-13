using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/WeaponPickUp")]
    public class WeaponPickUp : StateData
    {
        public float PickUpTiming;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime > PickUpTiming)
            {
                if (characterState.characterControl.animationProgress.HoldingWeapon == null)
                {
                    MeleeWeapon w = characterState.characterControl.animationProgress.GetTouchingWeapon();
                    characterState.characterControl.animationProgress.HoldingWeapon = w;

                    w.transform.parent = characterState.characterControl.RightHand_Attack.transform;
                    w.transform.localPosition = w.CustomPosition;
                    w.transform.localRotation = Quaternion.Euler(w.CustomRotation);

                    w.control = characterState.characterControl;
                    w.triggerDetector.control = characterState.characterControl;
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}