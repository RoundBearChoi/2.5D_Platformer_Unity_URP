using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AI/FallPlatform")]
    public class FallPlatform : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!characterState.characterControl.SkinnedMeshAnimator.GetBool(
                HashManager.Instance.ArrMainParams[(int)MainParameterType.Grounded]))
            {
                return;
            }

            if (characterState.characterControl.Attack)
            {
                return;
            }

            if (characterState.characterControl.transform.position.z <
                characterState.characterControl.aiProgress.pathfindingAgent.EndSphere.transform.position.z)
            {
                characterState.characterControl.MoveRight = true;
                characterState.characterControl.MoveLeft = false;
            }
            else if (characterState.characterControl.transform.position.z >
                characterState.characterControl.aiProgress.pathfindingAgent.EndSphere.transform.position.z)
            {
                characterState.characterControl.MoveRight = false;
                characterState.characterControl.MoveLeft = true;
            }

            if (characterState.characterControl.aiProgress.AIDistanceToStartSphere() > 3f)
            {
                characterState.characterControl.Turbo = true;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}