using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/MoveForward")]
    public class MoveForward : StateData
    {
        public bool debug;

        public bool AllowEarlyTurn;
        public bool LockDirection;
        public bool LockDirectionNextState;
        public bool Constant;
        public AnimationCurve SpeedGraph;
        public float Speed;
        public float BlockDistance;

        [Header("IgnoreCharacterBox")]
        public bool IgnoreCharacterBox;
        public float IgnoreStartTime;
        public float IgnoreEndTime;

        [Header("Momentum")]
        public bool UseMomentum;
        public float StartingMomentum;
        public float MaxMomentum;
        public bool ClearMomentumOnExit;
        
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.characterControl.animationProgress.LatestMoveForward = this;

            if (AllowEarlyTurn && !characterState.characterControl.animationProgress.disallowEarlyTurn)
            {
                if (!characterState.characterControl.animationProgress.LockDirectionNextState)
                {
                    if (characterState.characterControl.MoveLeft)
                    {
                        characterState.characterControl.FaceForward(false);
                    }
                    if (characterState.characterControl.MoveRight)
                    {
                        characterState.characterControl.FaceForward(true);
                    }
                }
            }

            if (StartingMomentum > 0.001f)
            {
                if (characterState.characterControl.IsFacingForward())
                {
                    characterState.characterControl.animationProgress.AirMomentum = StartingMomentum;
                }
                else
                {
                    characterState.characterControl.animationProgress.AirMomentum = -StartingMomentum;
                }
            }

            characterState.characterControl.animationProgress.disallowEarlyTurn = false;
            characterState.characterControl.animationProgress.LockDirectionNextState = false;
            //characterState.characterControl.animationProgress.BlockingObjs.Clear();
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (debug)
            {
                Debug.Log(stateInfo.normalizedTime);
            }

            characterState.characterControl.animationProgress.LockDirectionNextState = LockDirectionNextState;

            if (characterState.characterControl.animationProgress.
                LatestMoveForward != this)
            {
                return;
            }

            if (characterState.characterControl.animationProgress.
                IsRunning(typeof(WallSlide)))
            {
                return;
            }

            UpdateCharacterIgnoreTime(characterState.characterControl, stateInfo);

            if (characterState.characterControl.Jump)
            {
                if (characterState.characterControl.animationProgress.Ground != null)
                {
                    animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Jump], true);
                }
            }

            if (characterState.characterControl.Turbo)
            {
                animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Turbo], true);
            }
            else
            {
                animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Turbo], false);
            }

            if (UseMomentum)
            {
                UpdateMomentum(characterState.characterControl, stateInfo);
            }
            else
            {
                if (Constant)
                {
                    ConstantMove(characterState.characterControl, animator, stateInfo);
                }
                else
                {
                    ControlledMove(characterState.characterControl, animator, stateInfo);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (ClearMomentumOnExit)
            {
                characterState.characterControl.animationProgress.AirMomentum = 0f;
            }
        }

        private void UpdateMomentum(CharacterControl control, AnimatorStateInfo stateInfo)
        {
            if (!control.animationProgress.RightSideIsBlocked())
            {
                if (control.MoveRight)
                {
                    control.animationProgress.AirMomentum += SpeedGraph.Evaluate(stateInfo.normalizedTime) * Speed * Time.deltaTime;
                }
            }
            
            if (!control.animationProgress.LeftSideIsBlocked())
            {
                if (control.MoveLeft)
                {
                    control.animationProgress.AirMomentum -= SpeedGraph.Evaluate(stateInfo.normalizedTime) * Speed * Time.deltaTime;
                }
            }

            if (control.animationProgress.RightSideIsBlocked() ||
                control.animationProgress.LeftSideIsBlocked())
            {
                control.animationProgress.AirMomentum =
                    Mathf.Lerp(control.animationProgress.AirMomentum, 0f, Time.deltaTime * 1.5f);  
            }
            

            if (Mathf.Abs(control.animationProgress.AirMomentum) >= MaxMomentum)
            {
                if (control.animationProgress.AirMomentum > 0f)
                {
                    control.animationProgress.AirMomentum = MaxMomentum;
                }
                else if (control.animationProgress.AirMomentum < 0f)
                {
                    control.animationProgress.AirMomentum = -MaxMomentum;
                }
            }

            if (control.animationProgress.AirMomentum > 0f)
            {
                control.FaceForward(true);
            }
            else if (control.animationProgress.AirMomentum < 0f)
            {
                control.FaceForward(false);
            }

            if (!IsBlocked(control, Speed, stateInfo))
            {
                control.MoveForward(Speed, Mathf.Abs(control.animationProgress.AirMomentum));
            }
        }

        private void ConstantMove(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!IsBlocked(control, Speed, stateInfo))
            {
                control.MoveForward(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
            }

            if (!control.MoveRight && !control.MoveLeft)
            {
                animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], false);
            }
            else
            {
                animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], true);
            }
        }

        private void ControlledMove(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (control.MoveRight && control.MoveLeft)
            {
                animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], false);
                return;
            }

            if (!control.MoveRight && !control.MoveLeft)
            {
                animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], false);
                return;
            }

            if (control.MoveRight)
            {
                if (!IsBlocked(control, Speed, stateInfo))
                {
                    control.MoveForward(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
                }
            }

            if (control.MoveLeft)
            {
                if (!IsBlocked(control, Speed, stateInfo))
                {
                    control.MoveForward(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
                }
            }

            CheckTurn(control);
        }

        private void CheckTurn(CharacterControl control)
        {
            if (!LockDirection)
            {
                if (control.MoveRight)
                {
                    control.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }

                if (control.MoveLeft)
                {
                    control.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                }
            }
        }

        void UpdateCharacterIgnoreTime(CharacterControl control, AnimatorStateInfo stateInfo)
        {
            if (!IgnoreCharacterBox)
            {
                control.animationProgress.IsIgnoreCharacterTime = false;
            }

            if (stateInfo.normalizedTime > IgnoreStartTime &&
                stateInfo.normalizedTime < IgnoreEndTime)
            {
                control.animationProgress.IsIgnoreCharacterTime = true;
            }
            else
            {
                control.animationProgress.IsIgnoreCharacterTime = false;
            }
        }
           
        bool IsBlocked(CharacterControl control, float speed, AnimatorStateInfo stateInfo)
        {
            if (control.animationProgress.BlockingObjs.Count != 0)
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