using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AI/RestartAI")]
    public class RestartAI : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!AIIsOnGround(characterState.characterControl))
            {
                return;
            }

            // walking
            if (characterState.characterControl.aiProgress.AIDistanceToEndSphere() < 1f)
            {
                if (characterState.characterControl.aiProgress.TargetDistanceToEndSphere() > 0.5f)
                {
                    if (characterState.characterControl.aiProgress.TargetIsGrounded())
                    {
                        characterState.characterControl.aiController.InitializeAI();
                    }
                }
            }

            // landing
            if (characterState.characterControl.animationProgress.IsRunning(typeof(Landing)))
            {
                characterState.characterControl.Turbo = false;
                characterState.characterControl.Jump = false;
                characterState.characterControl.MoveUp = false;
                characterState.characterControl.aiController.InitializeAI();
            }

            // path is blocked
            if (characterState.BLOCKING_DATA.FrontBlockingDicCount == 0)
            {
                characterState.characterControl.aiProgress.BlockingCharacter = null;
            }
            else
            {
                List<GameObject> objs = characterState.BLOCKING_DATA.GetFrontBlockingCharacterList();

                foreach(GameObject o in objs)
                {
                    CharacterControl blockingChar = CharacterManager.Instance.GetCharacter(o);

                    if (blockingChar != null)
                    {
                        characterState.characterControl.aiProgress.BlockingCharacter = blockingChar;
                        break;
                    }
                    else
                    {
                        characterState.characterControl.aiProgress.BlockingCharacter = null;
                    }
                }
            }

            if (characterState.characterControl.aiProgress.BlockingCharacter != null)
            {
                if (characterState.characterControl.animationProgress.Ground != null)
                {
                    if (!characterState.characterControl.animationProgress.IsRunning(typeof(Jump)) &&
                        !characterState.characterControl.animationProgress.IsRunning(typeof(JumpPrep)))
                    {
                        characterState.characterControl.Turbo = false;
                        characterState.characterControl.Jump = false;
                        characterState.characterControl.MoveUp = false;
                        characterState.characterControl.MoveLeft = false;
                        characterState.characterControl.MoveRight = false;
                        characterState.characterControl.MoveDown = false;
                        characterState.characterControl.aiController.InitializeAI();
                    }
                }
            }

            //startsphere height
            if (characterState.characterControl.animationProgress.Ground != null &&
                !characterState.characterControl.animationProgress.IsRunning(typeof(Jump)) &&
                !characterState.characterControl.animationProgress.IsRunning(typeof(WallJumpPrep)))
            {
                if (characterState.characterControl.aiProgress.GetStartSphereHeight() > 0.1f)
                {
                    characterState.characterControl.Turbo = false;
                    characterState.characterControl.Jump = false;
                    characterState.characterControl.MoveUp = false;
                    characterState.characterControl.MoveLeft = false;
                    characterState.characterControl.MoveRight = false;
                    characterState.characterControl.MoveDown = false;
                    characterState.characterControl.aiController.InitializeAI();
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        bool AIIsOnGround(CharacterControl control)
        {
            if (control.animationProgress.IsRunning(typeof(MoveUp)))
            {
                return false;
            }

            if (control.RIGID_BODY.useGravity)
            {
                if (control.SkinnedMeshAnimator.GetBool(
                    HashManager.Instance.DicMainParams[TransitionParameter.Grounded]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}