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
        public bool AttackTriggered;
        public bool RagdollTriggered;
        public float MaxPressTime;

        [Header("GroundMovement")]
        public bool disallowEarlyTurn;
        public bool LockDirectionNextState;

        [Header("AirControl")]
        public bool Jumped;
        public float AirMomentum;
        //public bool FrameUpdated;
        public bool CancelPull;

        [Header("UpdateBoxCollider")]
        public bool UpdatingBoxCollider;
        public bool UpdatingSpheres;
        public Vector3 TargetSize;
        public float Size_Speed;
        public Vector3 TargetCenter;
        public float Center_Speed;

        private CharacterControl control;
        private float PressTime;

        private void Awake()
        {
            control = GetComponent<CharacterControl>();
            PressTime = 0f;
        }

        private void Update()
        {
            if (control.Attack)
            {
                PressTime += Time.deltaTime;
            }
            else
            {
                PressTime = 0f;
            }

            if (PressTime == 0f)
            {
                AttackTriggered = false;
            }
            else if(PressTime > MaxPressTime)
            {
                AttackTriggered = false;
            }
            else
            {
                AttackTriggered = true;
            }
        }

        //private void LateUpdate()
        //{
        //    FrameUpdated = false;
        //}

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
    }
}