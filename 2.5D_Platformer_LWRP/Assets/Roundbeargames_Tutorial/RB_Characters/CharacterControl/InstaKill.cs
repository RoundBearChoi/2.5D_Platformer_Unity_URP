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
            };

            subComponentProcessor.instaKillData = instaKillData;
            subComponentProcessor.ComponentsDic.Add(SubComponentType.INSTA_KILL, this);
        }

        public override void OnFixedUpdate()
        {
            if (control.subComponentProcessor.ComponentsDic.ContainsKey(SubComponentType.MANUALINPUT))
            {
                return;
            }

            if (!control.SkinnedMeshAnimator.GetBool(HashManager.Instance.DicMainParams[TransitionParameter.Grounded]))
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

                    if (!c.subComponentProcessor.ComponentsDic.ContainsKey(SubComponentType.MANUALINPUT))
                    {
                        continue;
                    }

                    if (!c.SkinnedMeshAnimator.GetBool(HashManager.Instance.DicMainParams[TransitionParameter.Grounded]))
                    {
                        continue;
                    }

                    if (c.animationProgress.IsRunning(typeof(Attack)))
                    {
                        continue;
                    }

                    if (control.animationProgress.IsRunning(typeof(Attack)))
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

                    Debug.Log("instaKill");
                    c.damageDetector.DeathByInstaKill(control);

                    return;
                }
            }
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}