using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CameraController : MonoBehaviour
    {
        public Cinemachine.CinemachineVirtualCamera DefaultCam;
        public float ZoomOutSpeed;
        public float ZoomInSpeed;

        private Animator animator;
        public Animator ANIMATOR
        {
            get
            {
                if (animator == null)
                {
                    animator = GetComponent<Animator>();
                }

                return animator;
            }
        }

        public void TriggerCamera(CameraTrigger trigger)
        {
            ANIMATOR.SetTrigger(HashManager.Instance.ArrCameraParams[(int)trigger]);
        }
    }
}
