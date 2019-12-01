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
                if (control.animationProgress.CollidingBodyParts.Count != 0)
                {
                    CheckAttack();
                }
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
            foreach(KeyValuePair<TriggerDetector, List<Collider>> data in
                control.animationProgress.CollidingBodyParts)
            {
                foreach(Collider collider in data.Value)
                {
                    foreach (AttackPartType part in info.AttackParts)
                    {
                        if (info.Attacker.GetAttackingPart(part) ==
                            collider.gameObject)
                        {
                            control.animationProgress.Attack = info.AttackAbility;
                            control.animationProgress.Attacker = info.Attacker;
                            control.animationProgress.DamagedTrigger = data.Key;
                            control.animationProgress.AttackingPart =
                                info.Attacker.GetAttackingPart(part);
                            return true;
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
                CameraManager.Instance.ShakeCamera(0.3f);

                if (info.AttackAbility.UseDeathParticles)
                {
                    if (info.AttackAbility.ParticleType.ToString().Contains("VFX"))
                    {
                        GameObject vfx =
                            PoolManager.Instance.GetObject(info.AttackAbility.ParticleType);

                        vfx.transform.position =
                            control.animationProgress.AttackingPart.transform.position;

                        vfx.SetActive(true);

                        if (info.Attacker.IsFacingForward())
                        {
                            vfx.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        }
                        else
                        {
                            vfx.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                        }
                    }
                }
            }

            Debug.Log(info.Attacker.gameObject.name + " hits: " + this.gameObject.name);

            info.CurrentHits++;
            DamageTaken++;

            AttackManager.Instance.ForceDeregister(control);

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