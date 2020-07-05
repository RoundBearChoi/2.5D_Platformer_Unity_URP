using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class SelectCameraState : StateMachineBehaviour
    {
        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PlayableCharacterType[] arr = System.Enum.GetValues(typeof(PlayableCharacterType)) as PlayableCharacterType[];

            foreach (PlayableCharacterType p in arr)
            {
                animator.SetBool(p.ToString(), false);
            }
        }
    }
}