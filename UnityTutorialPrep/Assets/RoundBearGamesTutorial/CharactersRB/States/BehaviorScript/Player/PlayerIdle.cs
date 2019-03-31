using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoundBearGames_ObstacleCourse
{
    public class PlayerIdle : CharacterStateBase
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (ControllerManager.Instance.MoveRight && ControllerManager.Instance.MoveLeft)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                return;
            }

            if (!ControllerManager.Instance.MoveRight && !ControllerManager.Instance.MoveLeft)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
            }

            if (ControllerManager.Instance.MoveRight)
            {
                GetCharacterControl(animator).FaceForward(true);
                animator.SetBool(TransitionParameter.Move.ToString(), true);
            }

            if (ControllerManager.Instance.MoveLeft)
            {
                GetCharacterControl(animator).FaceForward(false);
                animator.SetBool(TransitionParameter.Move.ToString(), true);
            }
        }
        
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        
        }
    }
}