using Roundbeargames.Datasets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/Jump")]
    public class Jump : StateData
    {
        [Range(0f, 1f)]
        public float JumpTiming;
        public float JumpForce;
        [Header("Extra Gravity")]
        //public AnimationCurve Pull;
        public bool CancelPull;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.characterControl.AIR_CONTROL.SetBool((int)AirControlBool.JUMPED, false);

            if (JumpTiming == 0f)
            {
                characterState.characterControl.RIGID_BODY.AddForce(Vector3.up * JumpForce);
                characterState.characterControl.AIR_CONTROL.SetBool((int)AirControlBool.JUMPED, true);
            }

            characterState.characterControl.AIR_CONTROL.SetBool((int)AirControlBool.CANCEL_PULL, CancelPull);
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            bool jumped = characterState.characterControl.AIR_CONTROL.GetBool((int)AirControlBool.JUMPED);

            if (!jumped && stateInfo.normalizedTime >= JumpTiming)
            {
                characterState.characterControl.RIGID_BODY.AddForce(Vector3.up * JumpForce);
                characterState.characterControl.AIR_CONTROL.SetBool((int)AirControlBool.JUMPED, true);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //characterState.characterControl.PullMultiplier = 0f;
        }
    }
}