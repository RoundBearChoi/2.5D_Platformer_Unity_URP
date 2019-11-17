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
            characterState.characterControl.Jump = true;
            characterState.characterControl.MoveUp = true;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.characterControl.Attack)
            {
                return;
            }

            float platformDist = characterState.characterControl.aiProgress.pathfindingAgent.EndSphere.transform.position.y -
                characterState.characterControl.collisionSpheres.FrontSpheres[0].transform.position.y;

            if (platformDist > 0.5f)
            {
                if (characterState.characterControl.aiProgress.pathfindingAgent.StartSphere.transform.position.z <
                characterState.characterControl.aiProgress.pathfindingAgent.EndSphere.transform.position.z)
                {
                    characterState.characterControl.MoveRight = true;
                    characterState.characterControl.MoveLeft = false;
                }
                else
                {
                    characterState.characterControl.MoveRight = false;
                    characterState.characterControl.MoveLeft = true;
                }
            }

            if (platformDist < 0.5f)
            {
                characterState.characterControl.MoveRight = false;
                characterState.characterControl.MoveLeft = false;
                characterState.characterControl.MoveUp = false;
                characterState.characterControl.Jump = false;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}