using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AI/JumpPlatform")]
    public class JumpPlatform : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            control.Jump = true;
            control.MoveUp = true;

            if(control.aiProgress.pathfindingAgent.StartSphere.transform.position.z
               < control.aiProgress.pathfindingAgent.EndSphere.transform.position.z)
            {
                control.FaceForward(true);
            }
            else
            {
                control.FaceForward(false);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            float topDist = control.aiProgress.pathfindingAgent.EndSphere.transform.position.y
                - control.FrontSpheres[1].transform.position.y;

            float bottomDist = control.aiProgress.pathfindingAgent.EndSphere.transform.position.y
                - control.FrontSpheres[0].transform.position.y;

            if (topDist < 1.5f && bottomDist > 0.5f)
            {
                if (control.IsFacingForward())
                {
                    control.MoveRight = true;
                    control.MoveLeft = false;
                }
                else
                {
                    control.MoveRight = false;
                    control.MoveLeft = true;
                }
            }

            if (bottomDist < 0.5f)
            {
                control.MoveRight = false;
                control.MoveLeft = false;
                control.MoveUp = false;
                control.Jump = false;

                animator.gameObject.SetActive(false);
                animator.gameObject.SetActive(true);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}