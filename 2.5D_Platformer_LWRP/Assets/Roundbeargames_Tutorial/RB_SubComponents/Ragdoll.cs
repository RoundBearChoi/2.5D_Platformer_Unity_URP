using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class Ragdoll : SubComponent
    {
        private void Start()
        {
            control.ProcDic.Add(CharacterProc.RAGDOLL_ON, TurnOnRagdoll);
        }

        public override void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        public void TurnOnRagdoll()
        {
            if (control.animationProgress.RagdollTriggered)
            {
                return;
            }

            control.animationProgress.RagdollTriggered = true;

            //change layers
            Transform[] arr = control.gameObject.GetComponentsInChildren<Transform>();
            foreach (Transform t in arr)
            {
                t.gameObject.layer = LayerMask.NameToLayer(RB_Layers.DEADBODY.ToString());
            }

            //save bodypart positions
            foreach (Collider c in control.BodyParts)
            {
                TriggerDetector det = c.GetComponent<TriggerDetector>();
                det.LastPosition = c.gameObject.transform.localPosition;
                det.LastRotation = c.gameObject.transform.localRotation;
            }

            //turn off animator/avatar
            control.RIGID_BODY.useGravity = false;
            control.RIGID_BODY.velocity = Vector3.zero;
            control.gameObject.GetComponent<BoxCollider>().enabled = false;
            control.SkinnedMeshAnimator.enabled = false;
            control.SkinnedMeshAnimator.avatar = null;

            //turn off ledge colliders
            control.ProcDic[CharacterProc.LEDGE_COLLIDERS_OFF]();

            //turn off ai
            if (control.aiController != null)
            {
                control.aiController.gameObject.SetActive(false);
                control.navMeshObstacle.enabled = false;
            }

            //turn on ragdoll
            foreach (Collider c in control.BodyParts)
            {
                c.isTrigger = false;

                TriggerDetector det = c.GetComponent<TriggerDetector>();
                c.transform.localPosition = det.LastPosition;
                c.transform.localRotation = det.LastRotation;

                c.attachedRigidbody.velocity = Vector3.zero;
            }

            control.AddForceToDamagedPart(false);
        }
    }
}