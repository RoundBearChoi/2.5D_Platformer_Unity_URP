using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class AnimationProgress : MonoBehaviour
    {
        public Dictionary<StateData, int> CurrentRunningAbilities =
            new Dictionary<StateData, int>();

        public bool CameraShaken;
        public List<PoolObjectType> SpawnedObjList = new List<PoolObjectType>();
        public bool RagdollTriggered;
        public MoveForward LatestMoveForward;

        [Header("Attack Button")]
        public bool AttackTriggered;
        public bool AttackButtonIsReset;

        [Header("GroundMovement")]
        public bool disallowEarlyTurn;
        public bool LockDirectionNextState;

        [Header("Colliding Objects")]
        public GameObject Ground;
        public GameObject BlockingObj;

        [Header("AirControl")]
        public bool Jumped;
        public float AirMomentum;
        public bool CancelPull;
        public Vector3 MaxFallVelocity;
        public bool CanWallJump;
        public bool CheckWallBlock;

        [Header("UpdateBoxCollider")]
        public bool UpdatingSpheres;
        public Vector3 TargetSize;
        public float Size_Speed;
        public Vector3 TargetCenter;
        public float Center_Speed;

        [Header("Damage Info")]
        public Attack Attack;
        public CharacterControl Attacker;
        public TriggerDetector DamagedTrigger;
        public GameObject AttackingPart;

        private CharacterControl control;

        private void Awake()
        {
            control = GetComponent<CharacterControl>();
        }

        private void Update()
        {
            if (control.Attack)
            {
                if (AttackButtonIsReset)
                {
                    AttackTriggered = true;
                    AttackButtonIsReset = false;
                }
            }
            else
            {
                AttackButtonIsReset = true;
                AttackTriggered = false;
            }
        }

        public bool IsRunning(System.Type type)
        {
            foreach(KeyValuePair<StateData, int> data in CurrentRunningAbilities)
            {
                if (data.Key.GetType() == type)
                {
                    return true;
                }
            }

            return false;
        }

        public bool RightSideIsBlocked()
        {
            if (BlockingObj == null)
            {
                return false;
            }

            if ((BlockingObj.transform.position -
                control.transform.position).z > 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool LeftSideIsBlocked()
        {
            if (BlockingObj == null)
            {
                return false;
            }

            if ((BlockingObj.transform.position -
                control.transform.position).z < 0f)
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