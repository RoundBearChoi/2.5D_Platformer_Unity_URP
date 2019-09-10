using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AI/FallPlatform")]
    public class FallPlatform : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.characterControl.transform.position.z 
                < characterState.characterControl.aiProgress.pathfindingAgent.EndSphere.transform.position.z)
            {
                characterState.characterControl.FaceForward(true);
            }
            else if (characterState.characterControl.transform.position.z 
                > characterState.characterControl.aiProgress.pathfindingAgent.EndSphere.transform.position.z)
            {
                characterState.characterControl.FaceForward(false);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.characterControl.IsFacingForward())
            {
                if (characterState.characterControl.transform.position.z 
                    < characterState.characterControl.aiProgress.pathfindingAgent.EndSphere.transform.position.z)
                {
                    characterState.characterControl.MoveRight = true;
                    characterState.characterControl.MoveLeft = false;
                }
                else
                {
                    characterState.characterControl.MoveRight = false;
                    characterState.characterControl.MoveLeft = false;

                    characterState.characterControl.aiController.InitializeAI();
                    //animator.gameObject.SetActive(false);
                    //animator.gameObject.SetActive(true);
                }
            }
            else
            {
                if (characterState.characterControl.transform.position.z 
                    > characterState.characterControl.aiProgress.pathfindingAgent.EndSphere.transform.position.z)
                {
                    characterState.characterControl.MoveRight = false;
                    characterState.characterControl.MoveLeft = true;
                }
                else
                {
                    characterState.characterControl.MoveRight = false;
                    characterState.characterControl.MoveLeft = false;

                    characterState.characterControl.aiController.InitializeAI();
                    //animator.gameObject.SetActive(false);
                    //animator.gameObject.SetActive(true);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}