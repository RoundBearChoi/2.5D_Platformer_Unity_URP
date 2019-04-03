﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class CharacterStateBase : StateMachineBehaviour
    {
        public List<StateData> ListAbilityData = new List<StateData>();

        public void UpdateAll(CharacterStateBase characterStateBase, Animator animator)
        {
            foreach(StateData d in ListAbilityData)
            {
                d.UpdateAbility(characterStateBase, animator);
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UpdateAll(this, animator);
        }

        private CharacterControl characterControl;
        public CharacterControl GetCharacterControl(Animator animator)
        {
            if (characterControl == null)
            {
                characterControl = animator.GetComponentInParent<CharacterControl>();
            }
            return characterControl;
        }
    }
}