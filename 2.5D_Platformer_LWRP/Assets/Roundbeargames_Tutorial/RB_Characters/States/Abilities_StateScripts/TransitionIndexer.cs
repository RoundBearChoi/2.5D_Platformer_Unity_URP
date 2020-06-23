using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum TransitionConditionType
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        ATTACK,
        JUMP,
        GRABBING_LEDGE,
        LEFT_OR_RIGHT,
        GROUNDED,
        MOVE_FORWARD,
        AIR,
        BLOCKED_BY_WALL,
        CAN_WALLJUMP,
        NOT_GRABBING_LEDGE,
        NOT_BLOCKED_BY_WALL,
        MOVING_TO_BLOCKING_OBJ,
        DOUBLE_TAP_UP,
        DOUBLE_TAP_DOWN,
        DOUBLE_TAP_LEFT,
        DOUBLE_TAP_RIGHT,
        TOUCHING_WEAPON,
        HOLDING_AXE,
        NOT_MOVING,
        RUN,
        NOT_RUN,
        BLOCKING,
        NOT_BLOCKING,
        ATTACK_IS_BLOCKED,
    }

    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/TransitionIndexer")]
    public class TransitionIndexer : StateData
    {
        public int Index;
        public List<TransitionConditionType> transitionConditions = new List<TransitionConditionType>();

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (TransitionConditionChecker.MakeTransition(characterState.characterControl, transitionConditions))
            {
                animator.SetInteger(HashManager.Instance.DicMainParams[TransitionParameter.TransitionIndex], Index);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.JUMP_DATA.CheckWallBlock = StartCheckingWallBlock();

            if (animator.GetInteger(HashManager.Instance.DicMainParams[TransitionParameter.TransitionIndex]) == 0)
            {
                if (!characterState.characterControl.animationProgress.LockTransition)
                {
                    if (TransitionConditionChecker.MakeTransition(characterState.characterControl, transitionConditions))
                    {
                        animator.SetInteger(HashManager.Instance.DicMainParams[TransitionParameter.TransitionIndex], Index);
                    }
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetInteger(HashManager.Instance.DicMainParams[TransitionParameter.TransitionIndex], 0);
        }

        private bool StartCheckingWallBlock()
        {
            foreach(TransitionConditionType t in transitionConditions)
            {
                if (t == TransitionConditionType.BLOCKED_BY_WALL ||
                    t == TransitionConditionType.NOT_BLOCKED_BY_WALL)
                {
                    return true;
                }
            }

            return false;
        }
    }
}