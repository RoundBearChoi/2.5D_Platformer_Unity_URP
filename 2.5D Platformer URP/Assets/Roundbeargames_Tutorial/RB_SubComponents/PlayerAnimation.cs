using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class PlayerAnimation : SubComponent
    {
        public AnimationData animationData;

        private void Start()
        {
            animationData = new AnimationData
            {
                InstantTransitionMade = false,
                CurrentRunningAbilities = new Dictionary<CharacterAbility, int>(),
                IsRunning = IsRunning,
            };

            subComponentProcessor.animationData = animationData;

            subComponentProcessor.ArrSubComponents[(int)SubComponentType.PLAYER_ANIMATION] = this;
            //subComponentProcessor.ComponentsDic.Add(SubComponentType.PLAYER_ANIMATION, this);
        }

        public override void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate()
        {
            if (IsRunning(typeof(LockTransition)))
            {
                if (control.animationProgress.LockTransition)
                {
                    control.SkinnedMeshAnimator.
                        SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.LockTransition],
                        true);
                }
                else
                {
                    control.SkinnedMeshAnimator.
                        SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.LockTransition],
                        false);
                }
            }
            else
            {
                control.SkinnedMeshAnimator.
                    SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.LockTransition],
                    false);
            }
        }

        bool IsRunning(System.Type type)
        {
            foreach (KeyValuePair<CharacterAbility, int> data in animationData.CurrentRunningAbilities)
            {
                if (data.Key.GetType() == type)
                {
                    return true;
                }
            }

            return false;
        }
    }
}