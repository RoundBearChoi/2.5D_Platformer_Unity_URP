using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace roundbeargames_tutorial
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AI/StartWalking")]
    public class StartWalking : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            Vector3 dir = control.aiProgress.pathfindingAgent.StartSphere.transform.position - control.transform.position;

            if (dir.z > 0f)
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

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            Vector3 dist = control.aiProgress.pathfindingAgent.StartSphere.transform.position - control.transform.position;

            if (Vector3.SqrMagnitude(dist) < 0.01f)
            {
                control.MoveRight = false;
                control.MoveLeft = false;

                if (control.aiProgress.pathfindingAgent.StartSphere.transform.position.y
                    < control.aiProgress.pathfindingAgent.EndSphere.transform.position.y)
                {
                    animator.SetBool(AI_Walk_Transitions.jump_platform.ToString(), true);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(AI_Walk_Transitions.jump_platform.ToString(), false);
        }
    }
}