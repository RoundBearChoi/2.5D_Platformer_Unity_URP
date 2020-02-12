using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class LedgeChecker : MonoBehaviour
    {
        public bool IsGrabbingLedge;
        public Ledge GrabbedLedge;
        public Vector3 LedgeCalibration = new Vector3();
        Ledge CheckLedge = null;
        CharacterControl control;

        public LedgeCollider Collider1;
        public LedgeCollider Collider2;

        private void Start()
        {
            IsGrabbingLedge = false;
            control = GetComponentInParent<CharacterControl>();
        }

        private void FixedUpdate()
        {
            if (!control.SkinnedMeshAnimator.GetBool(
                HashManager.Instance.DicMainParams[TransitionParameter.Grounded]))
            {
                foreach (GameObject obj in Collider1.CollidedObjects)
                {
                    if (!Collider2.CollidedObjects.Contains(obj))
                    {
                        IsGrabbingLedge = true;
                        break;
                    }
                    else
                    {
                        IsGrabbingLedge = false;
                    }
                }
            }
            else
            {
                IsGrabbingLedge = false;
            }

            if (Collider1.CollidedObjects.Count == 0)
            {
                IsGrabbingLedge = false;
            }
        }
    }
}