using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class LedgeChecker : SubComponent
    {
        public bool isGrabbingLedge;
        public Vector3 LedgeCalibration = new Vector3();

        public LedgeCollider Collider1;
        public LedgeCollider Collider2;

        public List<string> LedgeTriggerStateNames = new List<string>();

        private void Start()
        {
            isGrabbingLedge = false;

            subComponentProcessor.ComponentsDic.Add(SubComponents.LEDGECHECKER, this);

            control.ProcDic.Add(CharacterProc.LEDGE_COLLIDERS_OFF, LedgeCollidersOff);
            control.BoolDic.Add(BoolData.GRABBING_LEDGE, IsGrabbingLedge);
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
                    isGrabbingLedge = false;
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
                        isGrabbingLedge = false;
                    }
                }
            }
            else
            {
                isGrabbingLedge = false;
            }

            if (Collider1.CollidedObjects.Count == 0)
            {
                isGrabbingLedge = false;
            }
        }

        bool OffsetPosition(GameObject platform)
        {
            BoxCollider boxCollider = platform.GetComponent<BoxCollider>();

            if (boxCollider == null)
            {
                return false;
            }

            if (isGrabbingLedge)
            {
                return false;
            }

            isGrabbingLedge = true;
            control.RIGID_BODY.useGravity = false;
            control.RIGID_BODY.velocity = Vector3.zero;

            float y, z;
            y = platform.transform.position.y + (boxCollider.size.y / 2f);
            if (control.IsFacingForward())
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

            if (control.IsFacingForward())
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

        public bool IsGrabbingLedge()
        {
            return isGrabbingLedge;
        }
    }
}