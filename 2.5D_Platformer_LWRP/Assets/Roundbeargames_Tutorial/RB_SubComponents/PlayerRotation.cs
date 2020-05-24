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
                LockEarlyTurn = false,
                LockDirectionNextState = false,
                EarlyTurnIsLocked = EarlyTurnIsLocked,
                FaceForward = FaceForward,
                IsFacingForward = IsFacingForward,
            };

            subComponentProcessor.rotationData = rotationData;
        }

        public override void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        bool EarlyTurnIsLocked()
        {
            if (rotationData.LockEarlyTurn || rotationData.LockDirectionNextState)
            {
                return true;
            }
            else
            {
                return false;
            }
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
    }
}