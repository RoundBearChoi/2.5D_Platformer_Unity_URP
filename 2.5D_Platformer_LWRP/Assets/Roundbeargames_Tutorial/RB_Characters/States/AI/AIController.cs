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
            animatorController = this.gameObject.GetComponentInChildren<Animator>();
            control = this.gameObject.GetComponentInParent<CharacterControl>();
        }

        public void InitializeAI()
        {
            Debug.Log("Initializing AI...");
            animatorController.gameObject.SetActive(false);
            animatorController.gameObject.SetActive(true);
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

            if (info.shortNameHash == HashManager.Instance.DicAIStates[AI_States.AI_Attack])
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