using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CharacterState : StateMachineBehaviour
    {
        public List<StateData> ListAbilityData = new List<StateData>();
        public CharacterControl characterControl;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (characterControl == null)
            {
                characterControl = animator.transform.root.GetComponent<CharacterControl>();
                characterControl.CacheCharacterControl(animator);
            }

            foreach(StateData d in ListAbilityData)
            {
                d.OnEnter(this, animator, stateInfo);

                if (!characterControl.animationProgress.CurrentRunningAbilities.Contains(d))
                {
                    characterControl.animationProgress.CurrentRunningAbilities.Add(d);
                }
            }
        }

        public void UpdateAll(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            foreach(StateData d in ListAbilityData)
            {
                d.UpdateAbility(characterState, animator, stateInfo);
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UpdateAll(this, animator, stateInfo);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (StateData d in ListAbilityData)
            {
                d.OnExit(this, animator, stateInfo);

                if (characterControl.animationProgress.CurrentRunningAbilities.Contains(d))
                {
                    characterControl.animationProgress.CurrentRunningAbilities.Remove(d);
                }
            }
        }
    }
}