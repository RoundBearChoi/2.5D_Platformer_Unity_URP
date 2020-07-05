using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CameraManager : Singleton<CameraManager>
    {
        public Camera MainCamera;

        private Coroutine routine;
        private bool CamControllerInitiated = false;
        private CameraController cameraController;

        public CameraController CAM_CONTROLLER
        {
            get
            {
                if (!CamControllerInitiated)
                {
                    cameraController = GameObject.FindObjectOfType<CameraController>();
                    CamControllerInitiated = true;
                }
                return cameraController;
            }
        }

        private void Awake()
        {
            GameObject obj = GameObject.Find("Main Camera");
            MainCamera = obj.GetComponent<Camera>();
        }

        IEnumerator _CamShake(float sec)
        {
            if (CAM_CONTROLLER != null)
            {
                CAM_CONTROLLER.TriggerCamera(CameraTrigger.Shake);
                yield return new WaitForSeconds(sec);
                CAM_CONTROLLER.TriggerCamera(CameraTrigger.Default);
            }
        }

        public void ShakeCamera(float sec)
        {
            if (routine != null)
            {
                StopCoroutine(routine);
            }

            routine = StartCoroutine(_CamShake(sec));
        }
    }
}