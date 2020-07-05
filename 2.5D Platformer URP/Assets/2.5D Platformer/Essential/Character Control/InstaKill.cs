using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class InstaKill : SubComponent
    {
        public InstaKillData instaKillData;

        [SerializeField] RuntimeAnimatorController Assassination_A;
        [SerializeField] RuntimeAnimatorController Assassination_B;

        private void Start()
        {
            instaKillData = new InstaKillData
            {
                Animation_A = Assassination_A,
                Animation_B = Assassination_B,

                DeathByInstaKill = DeathByInstaKill,
            };

            subComponentProcessor.instaKillData = instaKillData;
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.INSTA_KILL] = this;
            //subComponentProcessor.ComponentsDic.Add(SubComponentType.INSTA_KILL, this);
        }

        public override void OnFixedUpdate()
        {
            if (control.subComponentProcessor.ArrSubComponents[(int)SubComponentType.MANUALINPUT] != null)
            {
                return;
            }
            //if (control.subComponentProcessor.ComponentsDic.ContainsKey(SubComponentType.MANUALINPUT))
            //{
            //    return;
            //}

            if (!control.SkinnedMeshAnimator.
                GetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Grounded]))
            {
                return;
            }

            foreach (KeyValuePair<TriggerDetector, List<Collider>> data in control.animationProgress.CollidingBodyParts)
            {
                foreach (Collider col in data.Value)
                {
                    CharacterControl c = CharacterManager.Instance.GetCharacter(col.transform.root.gameObject);

                    if (c == control)
                    {
                        continue;
                    }

                    if (c.subComponentProcessor.ArrSubComponents[(int)SubComponentType.MANUALINPUT] == null)
                    {
                        continue;
                    }

                    //if (!c.subComponentProcessor.ComponentsDic.ContainsKey(SubComponentType.MANUALINPUT))
                    //{
                    //    continue;
                    //}

                    if (!c.SkinnedMeshAnimator.GetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Grounded]))
                    {
                        continue;
                    }

                    if (c.ANIMATION_DATA.IsRunning(typeof(Attack)))
                    {
                        continue;
                    }

                    if (control.ANIMATION_DATA.IsRunning(typeof(Attack)))
                    {
                        continue;
                    }

                    if (c.animationProgress.StateNameContains("RunningSlide"))
                    {
                        continue;
                    }

                    if (c.DAMAGE_DATA.IsDead())
                    {
                        continue;
                    }

                    if (control.DAMAGE_DATA.IsDead())
                    {
                        continue;
                    }

                    //Debug.Log("instaKill");
                    //c.INSTA_KILL_DATA.DeathByInstaKill(control);

                    return;
                }
            }
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        void DeathByInstaKill(CharacterControl attacker)
        {
            control.ANIMATION_DATA.CurrentRunningAbilities.Clear();
            attacker.ANIMATION_DATA.CurrentRunningAbilities.Clear();

            control.RIGID_BODY.useGravity = false;
            control.boxCollider.enabled = false;
            control.SkinnedMeshAnimator.runtimeAnimatorController = control.INSTA_KILL_DATA.Animation_B;

            attacker.RIGID_BODY.useGravity = false;
            attacker.boxCollider.enabled = false;
            attacker.SkinnedMeshAnimator.runtimeAnimatorController = control.INSTA_KILL_DATA.Animation_A;

            Vector3 dir = control.transform.position - attacker.transform.position;

            if (dir.z < 0f)
            {
                attacker.ROTATION_DATA.FaceForward(false);
            }
            else if (dir.z > 0f)
            {
                attacker.ROTATION_DATA.FaceForward(true);
            }

            control.transform.LookAt(control.transform.position + (attacker.transform.forward * 5f), Vector3.up);
            control.transform.position = attacker.transform.position + (attacker.transform.forward * 0.45f);

            control.DAMAGE_DATA.hp = 0f;
        }
    }
}