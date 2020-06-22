using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class AIProgress : MonoBehaviour
    {
        [Range(0f, 1f)]
        public float FlyingKickProbability;

        public PathFindingAgent pathfindingAgent;
        public CharacterControl BlockingCharacter;
        public bool DoFlyingKick;

        delegate void GroundAttack(CharacterControl control);
        List<GroundAttack> ListGroundAttacks = new List<GroundAttack>();
        int AttackIndex;

        CharacterControl control;

        private void Awake()
        {
            control = this.gameObject.GetComponentInParent<CharacterControl>();

            ListGroundAttacks.Add(NormalGroundAttack);
            ListGroundAttacks.Add(ForwardGroundAttack);

            StartCoroutine(_RandomizeAttack());
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
                DAMAGE_DATA.IsDead())
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
                if (control.ROTATION_DATA.IsFacingForward())
                {
                    return true;
                }
            }
            else
            {
                if (!control.ROTATION_DATA.IsFacingForward())
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
            CharacterControl target = CharacterManager.Instance.GetCharacter(control.aiProgress.pathfindingAgent.target);

            if (target.GROUND_DATA.Ground == control.GROUND_DATA.Ground)
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
            CharacterControl target = CharacterManager.Instance.GetCharacter(control.aiProgress.pathfindingAgent.target);
            if (target.GROUND_DATA.Ground == null)
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
            if (Random.Range(0f, 1f) < FlyingKickProbability)
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

        public void DoAttack()
        {
            ListGroundAttacks[AttackIndex](control);
        }

        IEnumerator _RandomizeAttack()
        {
            while (true)
            {
                AttackIndex = Random.Range(0, ListGroundAttacks.Count);
                yield return new WaitForSeconds(2f);
            }
        }

        void NormalGroundAttack(CharacterControl control)
        {
            control.MoveRight = false;
            control.MoveLeft = false;

            control.ATTACK_DATA.AttackTriggered = true;
            control.Attack = false;
        }

        void ForwardGroundAttack(CharacterControl control)
        {
            if (control.aiProgress.TargetIsOnRightSide())
            {
                control.MoveRight = true;
                control.MoveLeft = false;

                ProcForwardGroundAttack(control);
            }
            else
            {
                control.MoveRight = false;
                control.MoveLeft = true;

                ProcForwardGroundAttack(control);
            }
        }

        void ProcForwardGroundAttack(CharacterControl control)
        {
            if (control.aiProgress.IsFacingTarget() &&
                    control.ANIMATION_DATA.IsRunning(typeof(MoveForward)))
            {
                control.ATTACK_DATA.AttackTriggered = true;
                control.Attack = false;
            }
        }
    }
}

