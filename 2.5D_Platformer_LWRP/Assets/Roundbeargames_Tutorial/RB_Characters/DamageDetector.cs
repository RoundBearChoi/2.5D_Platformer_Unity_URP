using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class DamageDetector : MonoBehaviour
    {
        CharacterControl control;
        public int DamageTaken;

        private void Awake()
        {
            DamageTaken = 0;
            control = GetComponent<CharacterControl>();
        }

        private void Update()
        {
            if (AttackManager.Instance.CurrentAttacks.Count > 0)
            {
                CheckAttack();
            }
        }

        private void CheckAttack()
        {
            foreach (AttackInfo info in AttackManager.Instance.CurrentAttacks)
            {
                if (info == null)
                {
                    continue;
                }

                if (!info.isRegisterd)
                {
                    continue;
                }

                if (info.isFinished)
                {
                    continue;
                }

                if (info.CurrentHits >= info.MaxHits)
                {
                    continue;
                }

                if (info.Attacker == control)
                {
                    continue;
                }

                if (info.MustFaceAttacker)
                {
                    Vector3 vec = this.transform.position - info.Attacker.transform.position;
                    if (vec.z * info.Attacker.transform.forward.z < 0f)
                    {
                        continue;
                    }
                }

                if (info.MustCollide)
                {
                    if (IsCollided(info))
                    {
                        TakeDamage(info);
                    }
                }
                else
                {
                    float dist = Vector3.SqrMagnitude(this.gameObject.transform.position - info.Attacker.transform.position);
                    if (dist <= info.LethalRange)
                    {
                        TakeDamage(info);
                    }
                }
            }
        }

        private bool IsCollided(AttackInfo info)
        {
            foreach (TriggerDetector trigger in control.GetAllTriggers())
            {
                foreach (Collider collider in trigger.CollidingParts)
                {
                    foreach (AttackPartType part in info.AttackParts)
                    {
                        if (part == AttackPartType.LEFT_HAND)
                        {
                            if (collider.gameObject == info.Attacker.LeftHand_Attack)
                            {
                                control.animationProgress.Attack = info.AttackAbility;
                                control.animationProgress.Attacker = info.Attacker;
                                control.animationProgress.DamagedTrigger = trigger;
                                return true;
                            }
                        }
                        else if (part == AttackPartType.RIGHT_HAND)
                        {
                            if (collider.gameObject == info.Attacker.RightHand_Attack)
                            {
                                control.animationProgress.Attack = info.AttackAbility;
                                control.animationProgress.Attacker = info.Attacker;
                                control.animationProgress.DamagedTrigger = trigger;
                                return true;
                            }
                        }
                        else if (part == AttackPartType.LEFT_FOOT)
                        {
                            if (collider.gameObject == info.Attacker.LeftFoot_Attack)
                            {
                                control.animationProgress.Attack = info.AttackAbility;
                                control.animationProgress.Attacker = info.Attacker;
                                control.animationProgress.DamagedTrigger = trigger;
                                return true;
                            }
                        }
                        else if (part == AttackPartType.RIGHT_FOOT)
                        {
                            if (collider.gameObject == info.Attacker.RightFoot_Attack)
                            {
                                control.animationProgress.Attack = info.AttackAbility;
                                control.animationProgress.Attacker = info.Attacker;
                                control.animationProgress.DamagedTrigger = trigger;
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        private void TakeDamage(AttackInfo info)
        {
            if (DamageTaken > 0)
            {
                return;
            }

            if (info.MustCollide)
            {
                CameraManager.Instance.ShakeCamera(0.2f);
            }

            Debug.Log(info.Attacker.gameObject.name + " hits: " + this.gameObject.name);

            info.CurrentHits++;
            DamageTaken++;

            control.animationProgress.RagdollTriggered = true;
            control.GetComponent<BoxCollider>().enabled = false;
            control.ledgeChecker.GetComponent<BoxCollider>().enabled = false;
            control.RIGID_BODY.useGravity = false;

            if (control.aiController != null)
            {
                control.aiController.gameObject.SetActive(false);
                control.navMeshObstacle.enabled = false;
            }
        }
    }
}