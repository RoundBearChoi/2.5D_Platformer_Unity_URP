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
                CurrentRunningAbilities = new Dictionary<StateData, int>(),
                IsRunning = IsRunning,
            };

            subComponentProcessor.animationData = animationData;

            subComponentProcessor.ComponentsDic.Add(SubComponentType.PLAYER_ANIMATION, this);
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
                        SetBool(HashManager.Instance.DicMainParams[TransitionParameter.LockTransition],
                        true);
                }
                else
                {
                    control.SkinnedMeshAnimator.
                        SetBool(HashManager.Instance.DicMainParams[TransitionParameter.LockTransition],
                        false);
                }
            }
            else
            {
                control.SkinnedMeshAnimator.
                    SetBool(HashManager.Instance.DicMainParams[TransitionParameter.LockTransition],
                    false);
            }
        }

        bool IsRunning(System.Type type)
        {
            foreach (KeyValuePair<StateData, int> data in animationData.CurrentRunningAbilities)
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