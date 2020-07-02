using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

namespace Roundbeargames
{
    public class AIController : MonoBehaviour
    {
        Vector3 TargetDir = new Vector3();
        CharacterControl control;
        Animator animatorController;

        public Animator ANIMATOR
        {
            get
            {
                return animatorController;
            }

            private set 
            {
            
            }
        }

        private void Awake()
        {
            control = this.gameObject.GetComponentInParent<CharacterControl>();
            animatorController = this.gameObject.GetComponentInChildren<Animator>();

            CharacterState[] arr = animatorController.GetBehaviours<CharacterState>();

            foreach(CharacterState aiState in arr)
            {
                aiState.characterControl = control;
            }
        }

        public void InitializeAI()
        {
            ANIMATOR.Play(HashManager.Instance.ArrAIStateNames[(int)AI_State_Name.SendPathfindingAgent], 0);
        }

        public void WalkStraightToStartSphere()
        {
            TargetDir = control.aiProgress.pathfindingAgent.
                StartSphere.transform.position -
                control.transform.position;

            if (TargetDir.z > 0f)
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

        public void WalkStraightToEndSphere()
        {
            TargetDir = control.aiProgress.pathfindingAgent.
                EndSphere.transform.position -
                control.transform.position;

            if (TargetDir.z > 0f)
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

        public bool RestartWalk()
        {
            if (control.aiProgress.AIDistanceToEndSphere() < 1f)
            {
                if (control.aiProgress.TargetDistanceToEndSphere() > 0.5f)
                {
                    if (control.aiProgress.TargetIsGrounded())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsAttacking()
        {
            AnimatorStateInfo info = control.aiController.ANIMATOR.GetCurrentAnimatorStateInfo(0);

            if (info.shortNameHash == HashManager.Instance.ArrAIStateNames[(int)AI_State_Name.AI_Attack])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}