using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ColliderRemover : MonoBehaviour
    {
        public void RemoveAllColliders()
        {
            Collider[] arr = this.gameObject.GetComponentsInChildren<Collider>();

            foreach (Collider c in arr)
            {
                DestroyImmediate(c);
            }
        }
    }
}