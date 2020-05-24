using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class LedgeChecker : SubComponent
    {
        public LedgeGrabData ledgeGrabData;

        [SerializeField] Vector3 LedgeCalibration = new Vector3();
        [SerializeField] LedgeCollider Collider1;
        [SerializeField] LedgeCollider Collider2;
        [SerializeField] List<string> LedgeTriggerStateNames = new List<string>();

        private void Start()
        {
            ledgeGrabData = new LedgeGrabData
            {
                isGrabbingLedge = false,
                LedgeCollidersOff = LedgeCollidersOff,
            };

            subComponentProcessor.ledgeGrabData = ledgeGrabData;
            subComponentProcessor.ComponentsDic.Add(SubComponentType.LEDGECHECKER, this);
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnFixedUpdate()
        {
            if (control.SkinnedMeshAnimator.GetBool(HashManager.Instance.DicMainParams[TransitionParameter.Grounded]))
            {
                if (control.RIGID_BODY.useGravity)
                {
                    ledgeGrabData.isGrabbingLedge = false;
                }
            }

            if (IsLedgeGrabCondition())
            {
                ProcLedgeGrab();
            }
        }

        bool IsLedgeGrabCondition()
        {
            if (!control.Jump)
            {
                return false;
            }

            foreach(string s in LedgeTriggerStateNames)
            {
                if (control.animationProgress.StateNameContains(s))
                {
                    return true;
                }
            }

            return false;
        }

        void ProcLedgeGrab()
        {
            if (!control.SkinnedMeshAnimator.GetBool(
                HashManager.Instance.DicMainParams[TransitionParameter.Grounded]))
            {
                foreach (GameObject obj in Collider1.CollidedObjects)
                {
                    if (!Collider2.CollidedObjects.Contains(obj))
                    {
                        if (OffsetPosition(obj))
                        {
                            break;
                        }
                    }
                    else
                    {
                        ledgeGrabData.isGrabbingLedge = false;
                    }
                }
            }
            else
            {
                ledgeGrabData.isGrabbingLedge = false;
            }

            if (Collider1.CollidedObjects.Count == 0)
            {
                ledgeGrabData.isGrabbingLedge = false;
            }
        }

        bool OffsetPosition(GameObject platform)
        {
            BoxCollider boxCollider = platform.GetComponent<BoxCollider>();

            if (boxCollider == null)
            {
                return false;
            }

            if (ledgeGrabData.isGrabbingLedge)
            {
                return false;
            }

            ledgeGrabData.isGrabbingLedge = true;
            control.RIGID_BODY.useGravity = false;
            control.RIGID_BODY.velocity = Vector3.zero;

            float y, z;
            y = platform.transform.position.y + (boxCollider.size.y / 2f);
            if (control.ROTATION_DATA.IsFacingForward())
            {
                z = platform.transform.position.z - (boxCollider.size.z / 2f);
            }
            else
            {
                z = platform.transform.position.z + (boxCollider.size.z / 2f);
            }

            Vector3 platformEdge = new Vector3(0f, y, z);

            GameObject TestingSphere = GameObject.Find("TestingSphere");
            TestingSphere.transform.position = platformEdge;

            if (control.ROTATION_DATA.IsFacingForward())
            {
                control.RIGID_BODY.MovePosition(
                    platformEdge + LedgeCalibration);
            }
            else
            {
                control.RIGID_BODY.MovePosition(
                    platformEdge + new Vector3(0f, LedgeCalibration.y, -LedgeCalibration.z));
            }

            return true;
        }

        public void LedgeCollidersOff()
        {
            Collider1.GetComponent<BoxCollider>().enabled = false;
            Collider2.GetComponent<BoxCollider>().enabled = false;
        }
    }
}