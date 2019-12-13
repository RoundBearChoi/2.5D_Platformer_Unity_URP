using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class MeleeWeapon : MonoBehaviour
    {
        public CharacterControl control;
        public BoxCollider PickUpCollider;
        public BoxCollider AttackCollider;
        public TriggerDetector triggerDetector;
        public Vector3 CustomPosition = new Vector3();
        public Vector3 CustomRotation = new Vector3();

        private void Update()
        {
            if (control != null)
            {
                PickUpCollider.enabled = false;
                AttackCollider.enabled = true;
            }
            else
            {
                PickUpCollider.enabled = true;
                AttackCollider.enabled = false;
            }
        }

        public static bool IsWeapon(GameObject obj)
        {
            if (obj.transform.root.gameObject.GetComponent<MeleeWeapon>() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DropWeapon()
        {
            MeleeWeapon w = control.animationProgress.HoldingWeapon;

            if (w != null)
            {
                w.transform.parent = null;

                if (control.IsFacingForward())
                {
                    w.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                }
                else
                {
                    w.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
                }
                
                w.transform.position = control.transform.position + (Vector3.up * 0.0225f);

                control.animationProgress.HoldingWeapon = null;
                control = null;
                w.triggerDetector.control = null;
            }
        }
    }
}