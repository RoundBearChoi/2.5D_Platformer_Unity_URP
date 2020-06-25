using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Roundbeargames
{
    public class CameraState : StateMachineBehaviour
    {
        CharacterControl MainCharacter;

        CinemachineTransposer Transposer; 
        float DefaultOffsetX;
        float ZoomOutOffsetX;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (Transposer == null)
            {
                Transposer = CameraManager.Instance.CAM_CONTROLLER.DefaultCam.GetCinemachineComponent<CinemachineTransposer>();
                DefaultOffsetX = Transposer.m_FollowOffset.x;
                ZoomOutOffsetX = DefaultOffsetX * 10f;
            }

            CameraTrigger[] arr = System.Enum.GetValues(typeof(CameraTrigger)) as CameraTrigger[];

            foreach (CameraTrigger t in arr)
            {
                CameraManager.Instance.CAM_CONTROLLER.ANIMATOR.
                    ResetTrigger(HashManager.Instance.DicCameraTriggers[t]);
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.shortNameHash == HashManager.Instance.DicCameraStates[Camera_States.Shake])
            {
                if (stateInfo.normalizedTime > 0.7f)
                {
                    animator.SetTrigger(HashManager.Instance.DicCameraTriggers[CameraTrigger.Default]);
                }
            }
            
            if (MainCharacter == null)
            {
                MainCharacter = CharacterManager.Instance.GetPlayableCharacter();
            }

            if (stateInfo.shortNameHash == HashManager.Instance.DicCameraStates[Camera_States.Default])
            {
                if (MainCharacter.SkinnedMeshAnimator.GetBool(
                    HashManager.Instance.DicMainParams[TransitionParameter.Grounded]))
                {
                    LerpNormal(CameraManager.Instance.CAM_CONTROLLER);
                }
                else
                {
                    if (MainCharacter.RIGID_BODY.velocity.y < 0f)
                    {
                        LerpZoomOut(CameraManager.Instance.CAM_CONTROLLER);
                    }
                }
            }
        }

        void LerpZoomOut(CameraController camCon)
        {
            if (Transposer != null)
            {
                if (Mathf.Abs(Transposer.m_FollowOffset.x - ZoomOutOffsetX) > 0.1f)
                {
                    //Debug.Log("lerping zoom out");
                    Transposer.m_FollowOffset.x = Mathf.Lerp(Transposer.m_FollowOffset.x, ZoomOutOffsetX,
                        Time.deltaTime * camCon.ZoomOutSpeed);
                }
            }
        }

        void LerpNormal(CameraController camCon)
        {
            if (Transposer != null)
            {
                if (Mathf.Abs(Transposer.m_FollowOffset.x - DefaultOffsetX) > 0.1f)
                {
                    //Debug.Log("lerping zoom int (back to default)");
                    Transposer.m_FollowOffset.x = Mathf.Lerp(Transposer.m_FollowOffset.x, DefaultOffsetX,
                        Time.deltaTime * camCon.ZoomInSpeed);
                }
            }
        }
    }
}