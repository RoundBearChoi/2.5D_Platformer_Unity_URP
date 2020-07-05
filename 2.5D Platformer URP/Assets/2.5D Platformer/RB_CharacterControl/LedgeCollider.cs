using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class LedgeCollider : MonoBehaviour
    {
        public List<GameObject> CollidedObjects = new List<GameObject>();

        private void OnTriggerEnter(Collider other)
        {
            if (!Ledge.IsCharacter(other.gameObject) &&
                !MeleeWeapon.IsWeapon(other.gameObject))
            {
                if (!CollidedObjects.Contains(other.gameObject))
                {
                    CollidedObjects.Add(other.gameObject);
                }
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (!Ledge.IsCharacter(other.gameObject) &&
                !MeleeWeapon.IsWeapon(other.gameObject))
            {
                if (CollidedObjects.Contains(other.gameObject))
                {
                    CollidedObjects.Remove(other.gameObject);
                }
            }  
        }
    }
}