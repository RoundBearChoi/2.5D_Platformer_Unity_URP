using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class AnimationProgress : MonoBehaviour
    {
        public bool CameraShaken;
        public List<PoolObjectType> SpawnedObjList = new List<PoolObjectType>();

        public MoveForward LatestMoveForward;
        public MoveUp LatestMoveUp;

        [Header("GroundMovement")]
        public bool IsIgnoreCharacterTime;

        [Header("Colliding Objects")]
        public Dictionary<TriggerDetector, List<Collider>> CollidingWeapons =
            new Dictionary<TriggerDetector, List<Collider>>();
        public Dictionary<TriggerDetector, List<Collider>> CollidingBodyParts =
            new Dictionary<TriggerDetector, List<Collider>>();
                
        [Header("Transition")]
        public bool LockTransition;

        [Header("Weapon")]
        public MeleeWeapon HoldingWeapon;

        private CharacterControl control;

        private void Awake()
        {
            control = GetComponent<CharacterControl>();
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
            Vector3 vec = control.DAMAGE_DATA.damageTaken.ATTACKER.transform.position -
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