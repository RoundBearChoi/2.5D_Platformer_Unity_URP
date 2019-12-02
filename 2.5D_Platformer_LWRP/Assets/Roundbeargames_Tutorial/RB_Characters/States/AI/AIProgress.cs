using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class AIProgress : MonoBehaviour
    {
        public PathFindingAgent pathfindingAgent;
        public CharacterControl BlockingCharacter;
        public bool DoFlyingKick;

        CharacterControl control;

        private void Awake()
        {
            control = this.gameObject.GetComponentInParent<CharacterControl>();
        }

        public float AIDistanceToStartSphere()
        {
            return Vector3.SqrMagnitude(
                control.aiProgress.pathfindingAgent.StartSphere.transform.position -
                control.transform.position);
        }

        public float AIDistanceToEndSphere()
        {
            return Vector3.SqrMagnitude(
                control.aiProgress.pathfindingAgent.EndSphere.transform.position -
                control.transform.position);
        }

        public float AIDistanceToTarget()
        {
            return Vector3.SqrMagnitude(
                control.aiProgress.pathfindingAgent.target.transform.position -
                control.transform.position);
        }
        
        public float TargetDistanceToEndSphere()
        {
            return Vector3.SqrMagnitude(
                control.aiProgress.pathfindingAgent.EndSphere.transform.position -
                control.aiProgress.pathfindingAgent.target.transform.position);
        }

        public bool TargetIsDead()
        {
            if (CharacterManager.Instance.GetCharacter(control.aiProgress.pathfindingAgent.target).
                damageDetector.DamageTaken > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TargetIsOnRightSide()
        {
            if ((control.aiProgress.pathfindingAgent.target.transform.position -
                control.transform.position).z > 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsFacingTarget()
        {
            if ((control.aiProgress.pathfindingAgent.target.transform.position -
                control.transform.position).z > 0f)
            {
                if (control.IsFacingForward())
                {
                    return true;
                }
            }
            else
            {
                if (!control.IsFacingForward())
                {
                    return true;
                }
            }

            return false;
        }

        public void RepositionDestination()
        {
            pathfindingAgent.StartSphere.transform.position = pathfindingAgent.target.transform.position;
            pathfindingAgent.EndSphere.transform.position = pathfindingAgent.target.transform.position;
        }

        public bool TargetIsOnSamePlatform()
        {
            if (CharacterManager.Instance.GetCharacter(control.aiProgress.pathfindingAgent.target).
                animationProgress.Ground ==
                control.animationProgress.Ground)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TargetIsGrounded()
        {
            if (CharacterManager.Instance.GetCharacter(control.aiProgress.pathfindingAgent.target).
                animationProgress.Ground == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool EndSphereIsHigher()
        {
            if (EndSphereIsStraight())
            {
                return false;
            }

            if (pathfindingAgent.EndSphere.transform.position.y -
                pathfindingAgent.StartSphere.transform.position.y > 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EndSphereIsLower()
        {
            if (EndSphereIsStraight())
            {
                return false;
            }

            if (pathfindingAgent.EndSphere.transform.position.y -
                pathfindingAgent.StartSphere.transform.position.y > 0f)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool EndSphereIsStraight()
        {
            if (Mathf.Abs(pathfindingAgent.EndSphere.transform.position.y -
                pathfindingAgent.StartSphere.transform.position.y) > 0.01f)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void SetRandomFlyingKick()
        {
            if (Random.Range(0f, 1f) < 0.3f)
            {
                DoFlyingKick = true;
            }
            else
            {
                DoFlyingKick = false;
            }
        }

        public float GetStartSphereHeight()
        {
            Vector3 vec = control.transform.position - pathfindingAgent.StartSphere.transform.position;

            return Mathf.Abs(vec.y);
        }
    }
}

