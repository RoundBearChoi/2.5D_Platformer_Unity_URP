using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/Death/TriggerRagdoll")]
    public class TriggerRagdoll : StateData
    {
        public float TriggerTiming;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //CharacterControl control = characterState.GetCharacterControl(animator);

            if (stateInfo.normalizedTime >= TriggerTiming)
            {
                characterState.characterControl.ProcDic[CharacterProc.RAGDOLL_ON]();
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //CharacterControl control = characterState.GetCharacterControl(animator);
            //control.animationProgress.RagdollTriggered = false;
        }
    }
}