using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace roundbeargames_tutorial
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AI/StartRunning")]
    public class StartRunning : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            Vector3 dir = control.aiProgress.pathfindingAgent.StartSphere.transform.position - control.transform.position;

            if (dir.z > 0f)
            {
                control.FaceForward(true);
                control.MoveRight = true;
                control.MoveLeft = false;
            }
            else
            {
                control.FaceForward(false);
                control.MoveRight = false;
                control.MoveLeft = true;
            }

            Vector3 dist = control.aiProgress.pathfindingAgent.StartSphere.transform.position - control.transform.position;

            if (Vector3.SqrMagnitude(dist) > 2f)
            {
                control.Turbo = true;
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            Vector3 dist = control.aiProgress.pathfindingAgent.StartSphere.transform.position - control.transform.position;

            if (Vector3.SqrMagnitude(dist) < 2f)
            {
                control.MoveRight = false;
                control.MoveLeft = false;
                control.Turbo = false;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}