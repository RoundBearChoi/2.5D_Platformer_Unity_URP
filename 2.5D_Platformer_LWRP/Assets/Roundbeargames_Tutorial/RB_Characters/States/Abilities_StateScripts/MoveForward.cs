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

        private List<GameObject> SpheresList;
        private float DirBlock;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
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
            if (debug)
            {
                Debug.Log(stateInfo.normalizedTime);
            }

            characterState.characterControl.animationProgress.LockDirectionNextState = LockDirectionNextState;

            if (characterState.characterControl.animationProgress.IsRunning(typeof(MoveForward), this))
            {
                return;
            }

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

        bool IgnoringCharacterBox(Collider col, AnimatorStateInfo stateInfo)
        {
            if (!IgnoreCharacterBox)
            {
                return false;
            }

            if (stateInfo.normalizedTime < IgnoreStartTime)
            {
                return false;
            }
            else if (stateInfo.normalizedTime > IgnoreEndTime)
            {
                return false;
            }

            if (col.transform.root.gameObject.GetComponent<CharacterControl>() != null)
            {
                return true;
            }

            return false;
        }

        bool IsBlocked(CharacterControl control, float speed, AnimatorStateInfo stateInfo)
        {
            if (speed > 0)
            {
                SpheresList = control.collisionSpheres.FrontSpheres;
                DirBlock = 0.3f;
            }
            else
            {
                SpheresList = control.collisionSpheres.BackSpheres;
                DirBlock = -0.3f;
            }

            foreach (GameObject o in SpheresList)
            {
                Debug.DrawRay(o.transform.position, control.transform.forward * DirBlock, Color.yellow);
                RaycastHit hit;
                if (Physics.Raycast(o.transform.position, control.transform.forward * DirBlock, out hit, BlockDistance))
                {
                    if (!IsBodyPart(hit.collider, control) &&
                        !IgnoringCharacterBox(hit.collider, stateInfo) &&
                        !Ledge.IsLedge(hit.collider.gameObject) &&
                        !Ledge.IsLedgeChecker(hit.collider.gameObject))
                    {
                        control.animationProgress.BlockingObj = hit.collider.transform.root.gameObject;
                        return true;
                    }
                }
            }

            control.animationProgress.BlockingObj = null;
            return false;
        }

        bool IsBodyPart(Collider col, CharacterControl control)
        {
            if (col.transform.root.gameObject == control.gameObject)
            {
                return true;
            }

            CharacterControl target = CharacterManager.Instance.GetCharacter(col.transform.root.gameObject);

            if (target == null)
            {
                return false;
            }

            if (target.damageDetector.DamageTaken > 0)
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