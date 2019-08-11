using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AI/StartRunning")]
    public class StartRunning : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //CharacterControl control = characterState.GetCharacterControl(animator);

            Vector3 dir = characterState.characterControl.aiProgress.pathfindingAgent.StartSphere.transform.position 
                - characterState.characterControl.transform.position;

            if (dir.z > 0f)
            {
                characterState.characterControl.FaceForward(true);
                characterState.characterControl.MoveRight = true;
                characterState.characterControl.MoveLeft = false;
            }
            else
            {
                characterState.characterControl.FaceForward(false);
                characterState.characterControl.MoveRight = false;
                characterState.characterControl.MoveLeft = true;
            }

            Vector3 dist = characterState.characterControl.aiProgress.pathfindingAgent.StartSphere.transform.position 
                - characterState.characterControl.transform.position;

            if (Vector3.SqrMagnitude(dist) > 2f)
            {
                characterState.characterControl.Turbo = true;
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //CharacterControl control = characterState.GetCharacterControl(animator);
            Vector3 dist = characterState.characterControl.aiProgress.pathfindingAgent.StartSphere.transform.position 
                - characterState.characterControl.transform.position;

            if (Vector3.SqrMagnitude(dist) < 2f)
            {
                characterState.characterControl.MoveRight = false;
                characterState.characterControl.MoveLeft = false;
                characterState.characterControl.Turbo = false;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}