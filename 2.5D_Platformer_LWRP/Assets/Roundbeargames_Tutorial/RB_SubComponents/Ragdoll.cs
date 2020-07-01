using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum RagdollPushType
    {
        NORMAL,
        DEAD_BODY,
    }

    public class Ragdoll : SubComponent
    {
        public RagdollData ragdollData;

        private void Start()
        {
            ragdollData = new RagdollData
            {
                RagdollTriggered = false,
                flyingRagdollData = new FlyingRagdollData(),

                GetBody = GetBodyPart,
                AddForceToDamagedPart = AddForceToDamagedPart,
                ClearExistingVelocity = ClearExistingVelocity,
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
                ragdollData.ArrBodyParts[i].attachedRigidbody.isKinematic = true;
            }

            for (int i = 0; i < ragdollData.ArrBodyParts.Length; i++)
            {
                TriggerDetector det = ragdollData.ArrBodyParts[i].GetComponent<TriggerDetector>();
                ragdollData.ArrBodyParts[i].attachedRigidbody.MovePosition(det.LastPosition);
                ragdollData.ArrBodyParts[i].attachedRigidbody.MoveRotation(det.LastRotation);
                ragdollData.ArrBodyParts[i].attachedRigidbody.velocity = Vector3.zero;
            }

            for (int i = 0; i < ragdollData.ArrBodyParts.Length; i++)
            {
                ragdollData.ArrBodyParts[i].attachedRigidbody.isKinematic = false;
            }
            
            ragdollData.ClearExistingVelocity();
            
            if (control.DAMAGE_DATA.damageTaken != null)
            {
                //take damage from ragdoll
                Vector3 incomingVelocity = control.DAMAGE_DATA.damageTaken.INCOMING_VELOCITY;
                TriggerDetector damagedPart = control.DAMAGE_DATA.damageTaken.DAMAGEE;

                if (Vector3.SqrMagnitude(incomingVelocity) > 0.0001f)
                {
                    Debug.Log(control.gameObject.name + ": taking damage from ragdoll");
                    damagedPart.body.AddForce(incomingVelocity * 0.7f);
                }

                //take damage from attack
                else
                {
                    ragdollData.AddForceToDamagedPart(RagdollPushType.NORMAL);
                }
            }
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

            return null;
        }

        void AddForceToDamagedPart(RagdollPushType pushType)
        {
            if (control.DAMAGE_DATA.damageTaken == null)
            {
                return;
            }

            if (control.DAMAGE_DATA.damageTaken.ATTACKER == null)
            {
                return;
            }

            DamageData damageData = control.DAMAGE_DATA;

            Vector3 forwardDir = damageData.damageTaken.ATTACKER.transform.forward;
            Vector3 rightDir = damageData.damageTaken.ATTACKER.transform.right;
            Vector3 upDir = damageData.damageTaken.ATTACKER.transform.up;

            Rigidbody body = control.DAMAGE_DATA.damageTaken.DAMAGEE.GetComponent<Rigidbody>();
            Attack attack = damageData.damageTaken.ATTACK;

            if (pushType == RagdollPushType.NORMAL)
            {
                body.AddForce(
                    forwardDir * attack.normalRagdollVelocity.ForwardForce +
                    rightDir * attack.normalRagdollVelocity.RightForce +
                    upDir * attack.normalRagdollVelocity.UpForce);
            }
            else if (pushType == RagdollPushType.DEAD_BODY)
            {
                body.AddForce(
                    forwardDir * attack.collateralRagdollVelocity.ForwardForce +
                    rightDir * attack.collateralRagdollVelocity.RightForce +
                    upDir * attack.collateralRagdollVelocity.UpForce);
            }
        }

        void ClearExistingVelocity()
        {
            for (int i = 0; i < ragdollData.ArrBodyParts.Length; i++)
            {
                ragdollData.ArrBodyParts[i].attachedRigidbody.velocity = Vector3.zero;
            }
        }
    }
}