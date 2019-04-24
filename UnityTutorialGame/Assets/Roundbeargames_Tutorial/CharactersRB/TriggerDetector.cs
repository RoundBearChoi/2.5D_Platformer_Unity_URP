using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public enum GeneralBodyPart
    {
        Upper,
        Lower,
        Arm,
        Leg,
    }

    public class TriggerDetector : MonoBehaviour
    {
        public GeneralBodyPart generalBodyPart;

        public List<Collider> CollidingParts = new List<Collider>();
        private CharacterControl owner;

        private void Awake()
        {
            owner = this.GetComponentInParent<CharacterControl>();
        }

        private void OnTriggerEnter(Collider col)
        {
            if (owner.RagdollParts.Contains(col))
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

            if (!CollidingParts.Contains(col))
            {
                CollidingParts.Add(col);
            }
        }

        private void OnTriggerExit(Collider attacker)
        {
            if (CollidingParts.Contains(attacker))
            {
                CollidingParts.Remove(attacker);
            }
        }
    }
}