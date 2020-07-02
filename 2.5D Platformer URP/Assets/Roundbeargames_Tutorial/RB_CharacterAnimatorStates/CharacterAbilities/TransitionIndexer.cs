using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum TransitionConditionType
    {
        UP = 0,
        DOWN = 1,
        LEFT = 2,
        RIGHT = 3,
        JUMP = 5,
        LEFT_OR_RIGHT = 7,
        MOVE_FORWARD = 9,
        RUN = 23,
        NOT_RUNNING = 24,
        NOT_MOVING = 22,
        NOT_TURBO = 28,

        DOUBLE_TAP_UP = 16,
        DOUBLE_TAP_DOWN = 17,
        DOUBLE_TAP_LEFT = 18,
        DOUBLE_TAP_RIGHT = 19,

        ATTACK = 4,
        
        GROUNDED = 8,
        NOT_GROUNDED = 10,

        GRABBING_LEDGE = 6,
        NOT_GRABBING_LEDGE = 13,

        BLOCKED_BY_WALL = 11,
        NOT_BLOCKED_BY_WALL = 14,
        CAN_WALLJUMP = 12,
        
        MOVING_TO_BLOCKING_OBJ = 15,

        TOUCHING_WEAPON = 20,
        HOLDING_AXE = 21,
        
        BLOCKING = 25,
        NOT_BLOCKING = 26,
        ATTACK_IS_BLOCKED = 27,
    }

    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/TransitionIndexer")]
    public class TransitionIndexer : CharacterAbility
    {
        public int Index;
        public List<TransitionConditionType> transitionConditions = new List<TransitionConditionType>();

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (TransitionConditionChecker.MakeTransition(characterState.characterControl, transitionConditions))
            {
                animator.SetInteger(HashManager.Instance.ArrMainParams[(int)MainParameterType.TransitionIndex], Index);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.JUMP_DATA.CheckWallBlock = StartCheckingWallBlock();

            if (animator.GetInteger(HashManager.Instance.ArrMainParams[(int)MainParameterType.TransitionIndex]) == 0)
            {
                if (!characterState.characterControl.animationProgress.LockTransition)
                {
                    if (TransitionConditionChecker.MakeTransition(characterState.characterControl, transitionConditions))
                    {
                        animator.SetInteger(HashManager.Instance.ArrMainParams[(int)MainParameterType.TransitionIndex], Index);
                    }
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetInteger(HashManager.Instance.ArrMainParams[(int)MainParameterType.TransitionIndex], 0);
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