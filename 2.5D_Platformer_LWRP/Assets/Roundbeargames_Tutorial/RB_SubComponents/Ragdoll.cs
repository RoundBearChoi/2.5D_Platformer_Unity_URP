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
                //BodyParts = new List<Collider>(),

                GetBody = GetBodyPart,
                AddForceToDamagedPart = AddForceToDamagedPart,
            };

            SetupBodyParts();
            subComponentProcessor.ragdollData = ragdollData;
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.RAGDOLL] = this;
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
            List<Collider> BodyParts = new List<Collider>();
            Collider[] colliders = control.gameObject.GetComponentsInChildren<Collider>();

            foreach (Collider c in colliders)
            {
                if (c.gameObject != control.gameObject)
                {
                    if (c.gameObject.GetComponent<LedgeChecker>() == null &&
                        c.gameObject.GetComponent<LedgeCollider>() == null)
                    {
                        c.isTrigger = true;
                        BodyParts.Add(c);
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

            ragdollData.ArrBodyParts = new Collider[BodyParts.Count];

            for (int i = 0; i < ragdollData.ArrBodyParts.Length; i++)
            {
                ragdollData.ArrBodyParts[i] = BodyParts[i];
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
            for (int i = 0; i < ragdollData.ArrBodyParts.Length; i++)
            {
                TriggerDetector det = ragdollData.ArrBodyParts[i].GetComponent<TriggerDetector>();
                det.LastPosition = ragdollData.ArrBodyParts[i].gameObject.transform.position;
                det.LastRotation = ragdollData.ArrBodyParts[i].gameObject.transform.rotation;
            }

            //foreach (Collider c in ragdollData.BodyParts)
            //{
            //    TriggerDetector det = c.GetComponent<TriggerDetector>();
            //    det.LastPosition = c.gameObject.transform.position;
            //    det.LastRotation = c.gameObject.transform.rotation;
            //}

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
            for (int i = 0; i < ragdollData.ArrBodyParts.Length; i++)
            {
                ragdollData.ArrBodyParts[i].isTrigger = false;

                TriggerDetector det = ragdollData.ArrBodyParts[i].GetComponent<TriggerDetector>();
                ragdollData.ArrBodyParts[i].attachedRigidbody.MovePosition(det.LastPosition);
                ragdollData.ArrBodyParts[i].attachedRigidbody.MoveRotation(det.LastRotation);
                ragdollData.ArrBodyParts[i].attachedRigidbody.velocity = Vector3.zero;
            }

            //foreach (Collider c in ragdollData.BodyParts)
            //{
            //    c.isTrigger = false;
            //
            //    TriggerDetector det = c.GetComponent<TriggerDetector>();
            //    c.attachedRigidbody.MovePosition(det.LastPosition);
            //    c.attachedRigidbody.MoveRotation(det.LastRotation);
            //
            //    c.attachedRigidbody.velocity = Vector3.zero;
            //}

            AddForceToDamagedPart(false);
        }

        Collider GetBodyPart(string name)
        {
            for (int i = 0; i < ragdollData.ArrBodyParts.Length; i++)
            {
                if (ragdollData.ArrBodyParts[i].name.Contains(name))
                {
                    return ragdollData.ArrBodyParts[i];
                }
            }

            //foreach (Collider c in ragdollData.BodyParts)
            //{
            //    if (c.name.Contains(name))
            //    {
            //        return c;
            //    }
            //}

            return null;
        }

        void AddForceToDamagedPart(bool zeroVelocity)
        {
            if (control.DAMAGE_DATA.DamagedTrigger != null)
            {
                if (zeroVelocity)
                {
                    for (int i = 0; i < ragdollData.ArrBodyParts.Length; i++)
                    {
                        ragdollData.ArrBodyParts[i].attachedRigidbody.velocity = Vector3.zero;
                    }

                    //foreach (Collider c in ragdollData.BodyParts)
                    //{
                    //    c.attachedRigidbody.velocity = Vector3.zero;
                    //}
                }

                control.DAMAGE_DATA.DamagedTrigger.GetComponent<Rigidbody>().
                    AddForce(control.DAMAGE_DATA.Attacker.transform.forward * control.DAMAGE_DATA.Attack.ForwardForce +
                    control.DAMAGE_DATA.Attacker.transform.right * control.DAMAGE_DATA.Attack.RightForce +
                    control.DAMAGE_DATA.Attacker.transform.up * control.DAMAGE_DATA.Attack.UpForce);
            }
        }
    }
}