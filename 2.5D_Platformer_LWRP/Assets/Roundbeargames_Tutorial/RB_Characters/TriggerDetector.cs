using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class TriggerDetector : MonoBehaviour
    {
        public CharacterControl control;

        public Vector3 LastPosition;
        public Quaternion LastRotation;

        private void Awake()
        {
            control = this.GetComponentInParent<CharacterControl>();
        }

        private void OnTriggerEnter(Collider col)
        {
            CheckCollidingBodyParts(col);
            CheckCollidingWeapons(col);
        }

        void CheckCollidingBodyParts(Collider col)
        {
            if (control.RagdollParts.Contains(col))
            {
                return;
            }

            CharacterControl attacker = col.transform.root.GetComponent<CharacterControl>();

            if (attacker == null)
            {
                return;
            }

            if (col.gameObject == attacker.gameObject)
            {
                return;
            }

            if (!control.animationProgress.CollidingBodyParts.ContainsKey(this))
            {
                control.animationProgress.CollidingBodyParts.Add(this, new List<Collider>());
            }

            if (!control.animationProgress.CollidingBodyParts[this].Contains(col))
            {
                control.animationProgress.CollidingBodyParts[this].Add(col);
            }
        }

        void CheckCollidingWeapons(Collider col)
        {
            if (col.transform.root.gameObject.GetComponent<MeleeWeapon>() == null)
            {
                return;
            }

            if (!control.animationProgress.CollidingWeapons.ContainsKey(this))
            {
                control.animationProgress.CollidingWeapons.Add(this, new List<Collider>());
            }

            if (!control.animationProgress.CollidingWeapons[this].Contains(col))
            {
                control.animationProgress.CollidingWeapons[this].Add(col);
            }
        }

        private void OnTriggerExit(Collider col)
        {
            CheckExitingBodyParts(col);
            CheckExitingWeapons(col);
        }

        void CheckExitingBodyParts(Collider col)
        {
            if (control == null)
            {
                return;
            }

            if (control.animationProgress.CollidingBodyParts.ContainsKey(this))
            {
                if (control.animationProgress.CollidingBodyParts[this].Contains(col))
                {
                    control.animationProgress.CollidingBodyParts[this].Remove(col);
                }

                if (control.animationProgress.CollidingBodyParts[this].Count == 0)
                {
                    control.animationProgress.CollidingBodyParts.Remove(this);
                }
            }
        }

        void CheckExitingWeapons(Collider col)
        {
            if (control == null)
            {
                return;
            }

            if (control.animationProgress.CollidingWeapons.ContainsKey(this))
            {
                if (control.animationProgress.CollidingWeapons[this].Contains(col))
                {
                    control.animationProgress.CollidingWeapons[this].Remove(col);
                }

                if (control.animationProgress.CollidingWeapons[this].Count == 0)
                {
                    control.animationProgress.CollidingWeapons.Remove(this);
                }
            }
        }
    }
}