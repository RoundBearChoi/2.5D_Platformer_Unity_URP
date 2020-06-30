using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AI/AIAttack")]
    public class AIAttack : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.characterControl.Turbo = false;
            characterState.characterControl.Attack = false;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!characterState.characterControl.aiProgress.TargetIsDead())
            {
                characterState.characterControl.aiProgress.DoAttack();
            }
            else
            {
                characterState.characterControl.MoveRight = false;
                characterState.characterControl.MoveLeft = false;
                characterState.characterControl.Attack = false;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}
