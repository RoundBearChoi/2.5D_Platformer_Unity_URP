using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class PlayerRotation : SubComponent
    {
        public RotationData rotationData;
        static string TutorialScene_CharacterSelect = "TutorialScene_CharacterSelect";

        private void Start()
        {
            rotationData = new RotationData
            {
                LockTurn = false,
                UnlockTiming = 0f,
                FaceForward = FaceForward,
                IsFacingForward = IsFacingForward,
            };

            subComponentProcessor.rotationData = rotationData;
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.PLAYER_ROTATION] = this;
        }

        public override void OnFixedUpdate()
        {
            ClearTurnLock();
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        void FaceForward(bool forward)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals(TutorialScene_CharacterSelect))
            {
                return;
            }

            if (!control.SkinnedMeshAnimator.enabled)
            {
                return;
            }

            if (control.ROTATION_DATA.LockTurn)
            {
                return;
            }

            if (forward)
            {
                control.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                control.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }

        bool IsFacingForward()
        {
            if (control.transform.forward.z > 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void ClearTurnLock()
        {
            if (!control.ANIMATION_DATA.IsRunning(typeof(LockTurn)))
            {
                if (rotationData.LockTurn)
                {
                    AnimatorStateInfo info = control.SkinnedMeshAnimator.GetCurrentAnimatorStateInfo(0);

                    if (info.normalizedTime >= rotationData.UnlockTiming)
                    {
                        rotationData.LockTurn = false;
                    }
                }
            }
        }
    }
}