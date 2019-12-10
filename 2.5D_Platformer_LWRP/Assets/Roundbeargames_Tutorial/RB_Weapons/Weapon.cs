using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class Weapon : MonoBehaviour
    {
        public Vector3 CustomPosition = new Vector3();
        public Vector3 CustomRotation = new Vector3();

        public static bool IsWeapon(GameObject obj)
        {
            if (obj.transform.root.gameObject.GetComponent<Weapon>() != null)
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