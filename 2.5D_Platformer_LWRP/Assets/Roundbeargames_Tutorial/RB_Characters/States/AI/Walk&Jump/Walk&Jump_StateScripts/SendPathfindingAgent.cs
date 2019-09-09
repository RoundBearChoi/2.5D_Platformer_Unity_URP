using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    public enum AI_Walk_Transitions
    {
        start_walking,
        jump_platform,
        fall_platform,

        start_running,
    }

    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AI/SendPathfindingAgent")]
    public class SendPathfindingAgent : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.characterControl.aiProgress.pathfindingAgent == null)
            {
                GameObject p = Instantiate(Resources.Load("PathfindingAgent", typeof(GameObject)) as GameObject);
                characterState.characterControl.aiProgress.pathfindingAgent = p.GetComponent<PathFindingAgent>();
            }

            characterState.characterControl.aiProgress.pathfindingAgent.owner = characterState.characterControl;
            characterState.characterControl.aiProgress.pathfindingAgent.GetComponent<NavMeshAgent>().enabled = false;
            characterState.characterControl.aiProgress.pathfindingAgent.transform.position = characterState.characterControl.transform.position;
            characterState.characterControl.navMeshObstacle.carving = false;
            characterState.characterControl.aiProgress.pathfindingAgent.GoToTarget();
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.characterControl.aiProgress.pathfindingAgent.StartWalk)
            {
                animator.SetBool(AI_Walk_Transitions.start_walking.ToString(), true);
                animator.SetBool(AI_Walk_Transitions.start_running.ToString(), true);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(AI_Walk_Transitions.start_walking.ToString(), false);
            animator.SetBool(AI_Walk_Transitions.start_running.ToString(), false);
        }
    }
}