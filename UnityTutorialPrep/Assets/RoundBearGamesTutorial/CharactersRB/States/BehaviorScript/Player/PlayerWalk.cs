using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoundBearGames_ObstacleCourse
{
    public class PlayerWalk : CharacterStateBase
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            AddAbility(AbilityFactory.Instance.GetAbility(AbilityType.WALK));
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UpdateAbilities(this, animator);
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        
        }
    }
}