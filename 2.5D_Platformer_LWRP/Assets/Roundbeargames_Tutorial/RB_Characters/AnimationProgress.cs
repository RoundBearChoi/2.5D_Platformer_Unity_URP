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

        public MoveForward LatestMoveForward;
        public MoveUp LatestMoveUp;

        [Header("Attack Button")]
        public bool AttackTriggered;
        public bool AttackButtonIsReset;

        [Header("GroundMovement")]
        public bool IsIgnoreCharacterTime;

        [Header("Colliding Objects")]
        public GameObject Ground;
        public Dictionary<TriggerDetector, List<Collider>> CollidingWeapons =
            new Dictionary<TriggerDetector, List<Collider>>();
        public Dictionary<TriggerDetector, List<Collider>> CollidingBodyParts =
            new Dictionary<TriggerDetector, List<Collider>>();

        public Vector3 CollidingPoint = new Vector3();
        
        [Header("Transition")]
        public bool LockTransition;

        [Header("Weapon")]
        public MeleeWeapon HoldingWeapon;

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

            if (IsRunning(typeof(LockTransition)))
            {
                if (control.animationProgress.LockTransition)
                {
                    control.SkinnedMeshAnimator.
                        SetBool(HashManager.Instance.DicMainParams[TransitionParameter.LockTransition],
                        true);
                }
                else
                {
                    control.SkinnedMeshAnimator.
                        SetBool(HashManager.Instance.DicMainParams[TransitionParameter.LockTransition],
                        false);
                }
            }
            else
            {
                control.SkinnedMeshAnimator.
                    SetBool(HashManager.Instance.DicMainParams[TransitionParameter.LockTransition],
                    false);
            }
        }

        public void NullifyUpVelocity()
        {
            control.RIGID_BODY.velocity = new Vector3(
                            control.RIGID_BODY.velocity.x,
                            0f,
                            control.RIGID_BODY.velocity.z);
        }

        public bool IsFacingAttacker()
        {
            Vector3 vec = control.DAMAGE_DATA.Attacker.transform.position -
                control.transform.position;

            if (vec.z < 0f)
            {
                if (control.ROTATION_DATA.IsFacingForward())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (vec.z > 0f)
            {
                if (control.ROTATION_DATA.IsFacingForward())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public bool ForwardIsReversed()
        {
            if (LatestMoveForward.MoveOnHit)
            {
                if (IsFacingAttacker())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (LatestMoveForward.Speed > 0f)
            {
                return false;
            }
            else if (LatestMoveForward.Speed < 0f)
            {
                return true;
            }

            return false;
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

        public bool StateNameContains(string str)
        {
            AnimatorClipInfo[] arr = control.SkinnedMeshAnimator.GetCurrentAnimatorClipInfo(0);

            foreach(AnimatorClipInfo clipInfo in arr)
            {
                if (clipInfo.clip.name.Contains(str))
                {
                    return true;
                }
            }

            return false;
        }

        public MeleeWeapon GetTouchingWeapon()
        {
            foreach(KeyValuePair<TriggerDetector, List<Collider>> data in CollidingWeapons)
            {
                MeleeWeapon w = data.Value[0].gameObject.GetComponent<MeleeWeapon>();
                return w;
            }

            return null;
        }
    }
}