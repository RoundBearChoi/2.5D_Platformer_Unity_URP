using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class AnimationProgress : MonoBehaviour
    {
        public bool Jumped;
        public bool CameraShaken;
        public List<PoolObjectType> SpawnedObjList = new List<PoolObjectType>();
        public bool AttackTriggered;
        public bool RagdollTriggered;
        public float MaxPressTime;
        public bool disallowEarlyTurn;
        public float AirMomentum;

        [Header("UpdateBoxCollider")]
        public bool UpdatingBoxCollider;
        public Vector3 TargeSize;
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
    }
}