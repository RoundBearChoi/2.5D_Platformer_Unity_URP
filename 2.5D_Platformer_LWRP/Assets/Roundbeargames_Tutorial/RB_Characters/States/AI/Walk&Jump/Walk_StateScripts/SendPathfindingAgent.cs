using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace roundbeargames_tutorial
{
    public enum AI_Walk_Transitions
    {
        start_walking,
        jump_platform,
    }

    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AI/SendPathfindingAgent")]
    public class SendPathfindingAgent : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.aiProgress.pathfindingAgent == null)
            {
                GameObject p = Instantiate(Resources.Load("PathfindingAgent", typeof(GameObject)) as GameObject);
                control.aiProgress.pathfindingAgent = p.GetComponent<PathFindingAgent>();
            }

            control.aiProgress.pathfindingAgent.GetComponent<NavMeshAgent>().enabled = false;
            control.aiProgress.pathfindingAgent.transform.position = control.transform.position;
            control.aiProgress.pathfindingAgent.GoToTarget();
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            if (control.aiProgress.pathfindingAgent.StartWalk)
            {
                animator.SetBool(AI_Walk_Transitions.start_walking.ToString(), true);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(AI_Walk_Transitions.start_walking.ToString(), false);
        }
    }
}