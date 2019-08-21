using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/MoveForward")]
    public class MoveForward : StateData
    {
        public bool AllowEarlyTurn;
        public bool LockDirection;
        public bool LockDirectionNextState;
        public bool Constant;
        public AnimationCurve SpeedGraph;
        public float Speed;
        public float BlockDistance;
        public bool IgnoreCharacterBox;

        [Header("Momentum")]
        public bool UseMomentum;
        public float StartingMomentum;
        public float MaxMomentum;
        public bool ClearMomentumOnExit;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //CharacterControl control = characterState.GetCharacterControl(animator);

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
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.characterControl.animationProgress.LockDirectionNextState = LockDirectionNextState;

            if (characterState.characterControl.animationProgress.IsRunning(typeof(MoveForward), this))
            {
                return;
            }

            //if (characterState.characterControl.animationProgress.FrameUpdated)
            //{
            //    return;
            //}

            //characterState.characterControl.animationProgress.FrameUpdated = true;

            if (characterState.characterControl.Jump)
            {
                animator.SetBool(TransitionParameter.Jump.ToString(), true);
            }

            if (characterState.characterControl.Turbo)
            {
                animator.SetBool(TransitionParameter.Turbo.ToString(), true);
            }
            else
            {
                animator.SetBool(TransitionParameter.Turbo.ToString(), false);
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
            //CharacterControl control = characterState.GetCharacterControl(animator);

            if (ClearMomentumOnExit)
            {
                characterState.characterControl.animationProgress.AirMomentum = 0f;
            }
        }

        private void UpdateMomentum(CharacterControl control, AnimatorStateInfo stateInfo)
        {
            if (control.MoveRight)
            {
                control.animationProgress.AirMomentum += SpeedGraph.Evaluate(stateInfo.normalizedTime) * Speed * Time.deltaTime;
            }

            if (control.MoveLeft)
            {
                control.animationProgress.AirMomentum -= SpeedGraph.Evaluate(stateInfo.normalizedTime) * Speed * Time.deltaTime;
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

            if (!CheckFront(control))
            {
                control.MoveForward(Speed, Mathf.Abs(control.animationProgress.AirMomentum));
            }
        }

        private void ConstantMove(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!CheckFront(control))
            {
                control.MoveForward(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
            }

            if (!control.MoveRight && !control.MoveLeft)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
            }
            else
            {
                animator.SetBool(TransitionParameter.Move.ToString(), true);
            }
        }

        private void ControlledMove(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (control.MoveRight && control.MoveLeft)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                return;
            }

            if (!control.MoveRight && !control.MoveLeft)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                return;
            }

            if (control.MoveRight)
            {
                if (!CheckFront(control))
                {
                    control.MoveForward(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
                }
            }

            if (control.MoveLeft)
            {
                if (!CheckFront(control))
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

        bool IgnoringCharacterBox(Collider col)
        {
            if (!IgnoreCharacterBox)
            {
                return false;
            }

            if (col.gameObject.GetComponent<CharacterControl>() != null)
            {
                return true;
            }

            return false;
        }

        bool CheckFront(CharacterControl control)
        {
            foreach (GameObject o in control.FrontSpheres)
            {
                Debug.DrawRay(o.transform.position, control.transform.forward * 0.3f, Color.yellow);
                RaycastHit hit;
                if (Physics.Raycast(o.transform.position, control.transform.forward, out hit, BlockDistance))
                {
                    if (!control.RagdollParts.Contains(hit.collider))
                    {
                        if (!IsBodyPart(hit.collider) 
                            && !Ledge.IsLedge(hit.collider.gameObject)
                            && !Ledge.IsLedgeChecker(hit.collider.gameObject)
                            && !IgnoringCharacterBox(hit.collider))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        bool IsBodyPart(Collider col)
        {
            CharacterControl control = col.transform.root.GetComponent<CharacterControl>();

            if (control == null)
            {
                return false;
            }

            if (control.gameObject == col.gameObject)
            {
                return false;
            }

            if (control.RagdollParts.Contains(col))
            {
                return true;
            }

            return false;
        }
    }
}