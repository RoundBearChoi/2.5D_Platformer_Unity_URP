using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class DamageDetector : SubComponent
    {
        public DamageData damageData;

        [Header("Damage Setup")]
        [SerializeField]
        Attack MarioStompAttack;
        [SerializeField]
        Attack AxeThrow;

        private void Start()
        {
            damageData = new DamageData
            {
                Attacker = null,
                Attack = null,
                DamagedTrigger = null,
                AttackingPart = null,
                BlockedAttack = null,
                hp = 3f,
                MarioStompAttack = MarioStompAttack,
                AxeThrow = AxeThrow,

                IsDead = IsDead,
                TakeDamage = TakeDamage,
            };

            subComponentProcessor.damageData = damageData;
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.DAMAGE_DETECTOR] = this;
            //subComponentProcessor.ComponentsDic.Add(SubComponentType.DAMAGE_DETECTOR, this);
        }

        public override void OnFixedUpdate()
        {
            if (AttackManager.Instance.CurrentAttacks.Count > 0)
            {
                CheckAttack();
            }
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        bool AttackIsValid(AttackCondition info)
        {
            if (info == null)
            {
                return false;
            }

            if (!info.isRegisterd)
            {
                return false;
            }

            if (info.isFinished)
            {
                return false;
            }

            if (info.CurrentHits >= info.MaxHits)
            {
                return false;
            }

            if (info.Attacker == control)
            {
                return false;
            }

            if (info.MustFaceAttacker)
            {
                Vector3 vec = this.transform.position - info.Attacker.transform.position;
                if (vec.z * info.Attacker.transform.forward.z < 0f)
                {
                    return false;
                }
            }

            if (info.RegisteredTargets.Contains(this.control))
            {
                return false;
            }

            return true;
        }

        void CheckAttack()
        {
            foreach (AttackCondition info in AttackManager.Instance.CurrentAttacks)
            {
                if (AttackIsValid(info))
                {
                    if (info.MustCollide)
                    {
                        if (control.animationProgress.CollidingBodyParts.Count != 0)
                        {
                            if (IsCollided(info))
                            {
                                TakeDamage(info);
                            }
                        }
                    }
                    else
                    {
                        if (IsInLethalRange(info))
                        {
                            TakeDamage(info);
                        }
                    }
                }
            }
        }

        bool IsCollided(AttackCondition info)
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
                            damageData.SetData(
                                info.Attacker,
                                info.AttackAbility,
                                data.Key,
                                info.Attacker.GetAttackingPart(part));

                            return true;
                        }
                    }
                }
            }

            return false;
        }

        bool IsInLethalRange(AttackCondition info)
        {
            for (int i = 0; i < control.RAGDOLL_DATA.ArrBodyParts.Length; i++)
            {
                float dist = Vector3.SqrMagnitude(
                    control.RAGDOLL_DATA.ArrBodyParts[i].transform.position - info.Attacker.transform.position);

                if (dist <= info.LethalRange)
                {
                    int index = Random.Range(0, control.RAGDOLL_DATA.ArrBodyParts.Length);
                    TriggerDetector triggerDetector = control.RAGDOLL_DATA.ArrBodyParts[index].GetComponent<TriggerDetector>();

                    damageData.SetData(
                        info.Attacker,
                        info.AttackAbility,
                        triggerDetector,
                        null);

                    return true;
                }
            }

            //foreach(Collider c in control.RAGDOLL_DATA.BodyParts)
            //{
            //    float dist = Vector3.SqrMagnitude(c.transform.position - info.Attacker.transform.position);
            //
            //    if (dist <= info.LethalRange)
            //    {
            //        int index = Random.Range(0, control.RAGDOLL_DATA.BodyParts.Count);
            //        TriggerDetector triggerDetector = control.RAGDOLL_DATA.BodyParts[index].GetComponent<TriggerDetector>();
            //
            //        damageData.SetData(
            //            info.Attacker,
            //            info.AttackAbility,
            //            triggerDetector,
            //            null);
            //
            //        return true;
            //    }
            //}

            return false;
        }

        bool IsDead()
        {
            if (damageData.hp <= 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool IsBlocked(AttackCondition info)
        {
            if (info == damageData.BlockedAttack && damageData.BlockedAttack != null)
            {
                return true;
            }

            if (control.ANIMATION_DATA.IsRunning(typeof(Block)))
            {
                Vector3 dir = info.Attacker.transform.position - control.transform.position;

                if (dir.z > 0f)
                {
                    if (control.ROTATION_DATA.IsFacingForward())
                    {
                        return true;
                    }
                }
                else if (dir.z < 0f)
                {
                    if (!control.ROTATION_DATA.IsFacingForward())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        void TakeDamage(AttackCondition info)
        {
            if (IsDead())
            {
                if (!info.RegisteredTargets.Contains(this.control))
                {
                    info.RegisteredTargets.Add(this.control);
                    control.RAGDOLL_DATA.AddForceToDamagedPart(true);
                }

                return;
            }

            if (IsBlocked(info))
            {
                damageData.BlockedAttack = info;
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
                            damageData.AttackingPart.GetComponent<Collider>().bounds.center;

                        //Debug.Log(control.gameObject.name + " damage detected: " + damageData.DamagedTrigger.gameObject.name);
                        //Debug.Log("attacking part: " + damageData.AttackingPart.gameObject.name);
                        //Debug.DrawLine(Vector3.zero, vfx.transform.position, Color.red, 60f);

                        vfx.SetActive(true);

                        if (info.Attacker.ROTATION_DATA.IsFacingForward())
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

            //Debug.Log(info.Attacker.gameObject.name + " hits: " + this.gameObject.name);

            info.CurrentHits++;
            damageData.hp -= info.AttackAbility.Damage;

            AttackManager.Instance.ForceDeregister(control);
            control.ANIMATION_DATA.CurrentRunningAbilities.Clear();

            if (IsDead())
            {
                control.RAGDOLL_DATA.RagdollTriggered = true;
            }
            else
            {
                int randomIndex = Random.Range(0, (int)Hit_Reaction_States.COUNT);
                control.SkinnedMeshAnimator.Play(HashManager.Instance.DicHitReactionStates[(Hit_Reaction_States)randomIndex], 0, 0f);
            }

            if (!info.RegisteredTargets.Contains(this.control))
            {
                info.RegisteredTargets.Add(this.control);
            }
        }
    }
}