using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/UpdateBoxCollider")]
    public class UpdateBoxCollider : CharacterAbility
    {
        public Vector3 TargetCenter;
        public float CenterUpdateSpeed;
        [Space(10)]
        public Vector3 TargetSize;
        public float SizeUpdateSpeed;

        const string LandingState = "Jump_Normal_Landing";
        const string ClimbingState = "LedgeClimb";

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.BOX_COLLIDER_DATA.TargetSize = TargetSize;
            characterState.BOX_COLLIDER_DATA.Size_Update_Speed = SizeUpdateSpeed;

            characterState.BOX_COLLIDER_DATA.TargetCenter = TargetCenter;
            characterState.BOX_COLLIDER_DATA.Center_Update_Speed = CenterUpdateSpeed;

            if (stateInfo.IsName(LandingState))
            {
                characterState.BOX_COLLIDER_DATA.IsLanding = true;
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.IsName(ClimbingState))
            {
                if (stateInfo.normalizedTime > 0.7f)
                {
                    if (animator.GetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Grounded]) == true)
                    {
                        characterState.BOX_COLLIDER_DATA.IsLanding = true;
                    }
                    else
                    {
                        characterState.BOX_COLLIDER_DATA.IsLanding = false;
                    }
                }
                else
                {
                    characterState.BOX_COLLIDER_DATA.IsLanding = false;
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.IsName(LandingState) ||
                stateInfo.IsName(ClimbingState))
            {
                characterState.BOX_COLLIDER_DATA.IsLanding = false;
            }
        }
    }
}