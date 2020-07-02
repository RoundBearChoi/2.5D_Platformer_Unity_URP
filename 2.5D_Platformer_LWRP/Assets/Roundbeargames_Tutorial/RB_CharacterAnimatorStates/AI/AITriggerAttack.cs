using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AI/AITriggerAttack")]
    public class AITriggerAttack : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.characterControl.aiProgress.TargetIsDead())
            {
                characterState.characterControl.Attack = false;
            }
            else
            {
                if (characterState.characterControl.aiProgress.AIDistanceToTarget() < 8f)
                {
                    if (!FlyingKick(characterState.characterControl))
                    {
                        if (characterState.characterControl.aiProgress.AIDistanceToTarget() < 2f)
                        {
                            TriggerAttack(characterState.characterControl);
                        }
                    }
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        bool FlyingKick(CharacterControl control)
        {
            if (control.aiProgress.DoFlyingKick &&
                control.aiProgress.TargetIsOnSamePlatform())
            {
                control.Attack = true;
                return true;
            }
            else
            {
                control.Attack = false;
                return false;
            }
        }

        void TriggerAttack(CharacterControl control)
        {
            control.aiController.ANIMATOR.Play(HashManager.Instance.ArrAIStateNames[(int)AI_State_Name.AI_Attack], 0);
        }
    }
}