using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class AnimationProgress : MonoBehaviour
    {
        public List<StateData> CurrentRunningAbilities = new List<StateData>();

        public bool CameraShaken;
        public List<PoolObjectType> SpawnedObjList = new List<PoolObjectType>();
        public bool RagdollTriggered;

        [Header("Attack Button")]
        public bool AttackTriggered;
        public bool AttackButtonIsReset;

        [Header("GroundMovement")]
        public bool disallowEarlyTurn;
        public bool LockDirectionNextState;
        public bool IsLanding;

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
        public bool UpdatingBoxCollider;
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

        public bool IsRunning(System.Type type, StateData self)
        {
            for (int i = 0; i < CurrentRunningAbilities.Count; i++)
            {
                if (type == CurrentRunningAbilities[i].GetType())
                {
                    if (CurrentRunningAbilities[i] == self)
                    {
                        return false;
                    }
                    else
                    {
                        //Debug.Log(type.ToString() + " is already running");
                        return true;
                    }
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