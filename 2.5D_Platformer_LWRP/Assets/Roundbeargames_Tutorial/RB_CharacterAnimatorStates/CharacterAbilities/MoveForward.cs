using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/MoveForward")]
    public class MoveForward : CharacterAbility
    {
        public bool debug;

        public bool AllowEarlyTurn;
        public bool LockDirection;
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
        public bool StartFromPreviousMomentum;
        public bool ClearMomentumOnExit;

        [Header("MoveOnHit")]
        public bool MoveOnHit;
        
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.characterControl.animationProgress.LatestMoveForward = this;

            if (AllowEarlyTurn)
            {
                if (characterState.characterControl.MoveLeft)
                {
                    characterState.ROTATION_DATA.FaceForward(false);
                }
                if (characterState.characterControl.MoveRight)
                {
                    characterState.ROTATION_DATA.FaceForward(true);
                }
            }

            if (!StartFromPreviousMomentum)
            {
                if (StartingMomentum > 0.001f)
                {
                    if (characterState.ROTATION_DATA.IsFacingForward())
                    {
                        characterState.MOMENTUM_DATA.Momentum = StartingMomentum;
                    }
                    else
                    {
                        characterState.MOMENTUM_DATA.Momentum = -StartingMomentum;
                    }
                }
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (debug)
            {
                Debug.Log(stateInfo.normalizedTime);
            }

            if (characterState.characterControl.animationProgress.LatestMoveForward != this)
            {
                return;
            }

            if (characterState.ANIMATION_DATA.IsRunning(typeof(WallSlide)))
            {
                return;
            }

            UpdateCharacterIgnoreTime(characterState.characterControl, stateInfo);

            if (characterState.characterControl.Turbo)
            {
                animator.SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Turbo], true);
            }
            else
            {
                animator.SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Turbo], false);
            }

            if (UseMomentum)
            {
                MoveOnMomentum(characterState.characterControl, stateInfo);
            }
            else
            {
                if (Constant)
                {
                    ConstantMove(characterState.characterControl, stateInfo);
                }
                else
                {
                    ControlledMove(characterState.characterControl, stateInfo);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (ClearMomentumOnExit)
            {
                characterState.MOMENTUM_DATA.Momentum = 0f;
            }
        }

        private void MoveOnMomentum(CharacterControl control, AnimatorStateInfo stateInfo)
        {
            float speed = SpeedGraph.Evaluate(stateInfo.normalizedTime) * Speed * Time.deltaTime;
            control.MOMENTUM_DATA.CalculateMomentum(speed, MaxMomentum);

            if (control.MOMENTUM_DATA.Momentum > 0f)
            {
                control.ROTATION_DATA.FaceForward(true);
            }
            else if (control.MOMENTUM_DATA.Momentum < 0f)
            {
                control.ROTATION_DATA.FaceForward(false);
            }

            if (!IsBlocked(control))
            {
                control.MoveForward(Speed, Mathf.Abs(control.MOMENTUM_DATA.Momentum));
            }
        }

        private void ConstantMove(CharacterControl control, AnimatorStateInfo stateInfo)
        {
            if (!IsBlocked(control))
            {
                if (MoveOnHit)
                {
                    if (!control.animationProgress.IsFacingAttacker())
                    {
                        control.MoveForward(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
                    }
                    else
                    {
                        control.MoveForward(-Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
                    }
                }
                else
                {
                    control.MoveForward(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
                }
            }
        }

        private void ControlledMove(CharacterControl control, AnimatorStateInfo stateInfo)
        {
            if (control.MoveRight && control.MoveLeft)
            {
                return;
            }

            if (!control.MoveRight && !control.MoveLeft)
            {
                return;
            }

            if (control.MoveRight)
            {
                if (!IsBlocked(control))
                {
                    control.MoveForward(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
                }
            }

            if (control.MoveLeft)
            {
                if (!IsBlocked(control))
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
                    control.ROTATION_DATA.FaceForward(true);
                }

                if (control.MoveLeft)
                {
                    control.ROTATION_DATA.FaceForward(false);
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
           
        bool IsBlocked(CharacterControl control)
        {
            if (control.BLOCKING_DATA.FrontBlockingDicCount != 0)
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