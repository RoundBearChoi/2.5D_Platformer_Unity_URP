using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class Ragdoll : SubComponent
    {
        public RagdollData ragdollData;

        private void Start()
        {
            ragdollData = new RagdollData
            {
                RagdollTriggered = false,
                BodyParts = new List<Collider>(),
                GetBody = GetBodyPart,
            };

            SetupBodyParts();
            subComponentProcessor.ragdollData = ragdollData;
            subComponentProcessor.ComponentsDic.Add(SubComponentType.RAGDOLL, this);
        }

        public override void OnFixedUpdate()
        {
            if (ragdollData.RagdollTriggered)
            {
                ProcRagdoll();
            }
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        public void SetupBodyParts()
        {
            ragdollData.BodyParts.Clear();

            Collider[] colliders = control.gameObject.GetComponentsInChildren<Collider>();

            foreach (Collider c in colliders)
            {
                if (c.gameObject != control.gameObject)
                {
                    if (c.gameObject.GetComponent<LedgeChecker>() == null &&
                        c.gameObject.GetComponent<LedgeCollider>() == null)
                    {
                        c.isTrigger = true;
                        ragdollData.BodyParts.Add(c);
                        c.attachedRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
                        c.attachedRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;

                        CharacterJoint joint = c.GetComponent<CharacterJoint>();
                        if (joint != null)
                        {
                            joint.enableProjection = true;
                        }

                        if (c.GetComponent<TriggerDetector>() == null)
                        {
                            c.gameObject.AddComponent<TriggerDetector>();
                        }
                    }
                }
            }
        }

        void ProcRagdoll()
        {
            ragdollData.RagdollTriggered = false;

            if (control.SkinnedMeshAnimator.avatar == null)
            {
                return;
            }

            //change layers
            Transform[] arr = control.gameObject.GetComponentsInChildren<Transform>();
            foreach (Transform t in arr)
            {
                t.gameObject.layer = LayerMask.NameToLayer(RB_Layers.DEADBODY.ToString());
            }

            //save bodypart positions
            foreach (Collider c in ragdollData.BodyParts)
            {
                TriggerDetector det = c.GetComponent<TriggerDetector>();
                det.LastPosition = c.gameObject.transform.position;
                det.LastRotation = c.gameObject.transform.rotation;
            }

            //turn off animator/avatar
            control.RIGID_BODY.useGravity = false;
            control.RIGID_BODY.velocity = Vector3.zero;
            control.gameObject.GetComponent<BoxCollider>().enabled = false;
            control.SkinnedMeshAnimator.enabled = false;
            control.SkinnedMeshAnimator.avatar = null;

            //turn off ledge colliders
            control.LEDGE_GRAB_DATA.LedgeCollidersOff();

            //turn off ai
            if (control.aiController != null)
            {
                control.aiController.gameObject.SetActive(false);
                control.navMeshObstacle.enabled = false;
            }

            //turn on ragdoll
            foreach (Collider c in ragdollData.BodyParts)
            {
                c.isTrigger = false;

                TriggerDetector det = c.GetComponent<TriggerDetector>();
                c.attachedRigidbody.MovePosition(det.LastPosition);
                c.attachedRigidbody.MoveRotation(det.LastRotation);

                c.attachedRigidbody.velocity = Vector3.zero;
            }

            control.AddForceToDamagedPart(false);
        }

        Collider GetBodyPart(string name)
        {
            foreach (Collider c in ragdollData.BodyParts)
            {
                if (c.name.Contains(name))
                {
                    return c;
                }
            }

            return null;
        }
    }
}