using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class TriggerDetector : MonoBehaviour
    {
        private CharacterControl control;

        public Vector3 LastPosition;
        public Quaternion LastRotation;

        private void Awake()
        {
            control = this.GetComponentInParent<CharacterControl>();
        }

        private void OnTriggerEnter(Collider col)
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

        private void OnTriggerExit(Collider attackingBodyPart)
        {
            if (control.animationProgress.CollidingBodyParts.ContainsKey(this))
            {
                if (control.animationProgress.CollidingBodyParts[this].Contains(attackingBodyPart))
                {
                    control.animationProgress.CollidingBodyParts[this].Remove(attackingBodyPart);
                }

                if (control.animationProgress.CollidingBodyParts[this].Count == 0)
                {
                    control.animationProgress.CollidingBodyParts.Remove(this);
                }
            }
        }
    }
}