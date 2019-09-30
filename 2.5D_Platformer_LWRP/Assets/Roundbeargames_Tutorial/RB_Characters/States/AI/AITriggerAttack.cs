using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AI/AITriggerAttack")]
    public class AITriggerAttack : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.characterControl.Turbo &&
                characterState.characterControl.aiProgress.DoFlyingKick &&
                characterState.characterControl.aiProgress.TargetIsOnSamePlatform() &&
                characterState.characterControl.aiProgress.AIDistanceToTarget() < 2f &&
                !characterState.characterControl.aiProgress.TargetIsDead())
            {
                characterState.characterControl.Attack = true;
            }
            else
            {
                characterState.characterControl.Attack = false;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}