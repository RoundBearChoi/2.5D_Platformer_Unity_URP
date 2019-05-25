using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class DeathAnimationManager : Singleton<DeathAnimationManager>
    {
        DeathAnimationLoader deathAnimationLoader;
        List<RuntimeAnimatorController> Candidates = new List<RuntimeAnimatorController>();

        void SetupDeathAnimationLoader()
        {
            if (deathAnimationLoader == null)
            {
                GameObject obj = Instantiate(Resources.Load("DeathAnimationLoader", typeof(GameObject)) as GameObject);
                DeathAnimationLoader loader = obj.GetComponent<DeathAnimationLoader>();

                deathAnimationLoader = loader;
            }
        }

        public RuntimeAnimatorController GetAnimator(GeneralBodyPart generalBodyPart, AttackInfo info)
        {
            SetupDeathAnimationLoader();

            Candidates.Clear();

            foreach (DeathAnimationData data in deathAnimationLoader.DeathAnimationDataList)
            {
                if (info.deathType == data.deathType)
                {
                    if (info.deathType != DeathType.NONE)
                    {
                        Candidates.Add(data.Animator);
                    }
                    else if (!info.MustCollide)
                    {
                        Candidates.Add(data.Animator);
                    }
                    else
                    {
                        foreach (GeneralBodyPart part in data.GeneralBodyParts)
                        {
                            if (part == generalBodyPart)
                            {
                                Candidates.Add(data.Animator);
                                break;
                            }
                        }
                    }
                }
            }

            return Candidates[Random.Range(0, Candidates.Count)];
        }
    }
}