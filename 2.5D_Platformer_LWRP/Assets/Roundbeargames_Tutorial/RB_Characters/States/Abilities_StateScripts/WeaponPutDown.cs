using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/WeaponPutDown")]
    public class WeaponPutDown : StateData
    {
        public float PutDownTiming;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime > PutDownTiming)
            {
                if (characterState.characterControl.animationProgress.HoldingWeapon != null)
                {
                    characterState.characterControl.animationProgress.HoldingWeapon.DropWeapon();
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}