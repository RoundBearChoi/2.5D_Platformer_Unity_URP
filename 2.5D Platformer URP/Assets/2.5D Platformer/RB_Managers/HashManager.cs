using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum MainParameterType
    {
        Move,
        Left,
        Right,
        Up,
        Down,
        Jump,
        ForceTransition,
        Grounded,
        ClickAnimation,
        TransitionIndex,
        Turbo,
        Turn,
        LockTransition,

        COUNT,
    }

    public enum CameraTrigger
    {
        Default,
        Shake,

        COUNT,
    }

    public enum AI_Transition
    {
        start_walking,
        jump_platform,
        fall_platform,

        COUNT,
    }

    public enum AI_State_Name
    {
        SendPathfindingAgent,
        AI_Attack,

        COUNT,
    }

    public enum Hit_Reaction_States
    {
        Head_Hit,
        Zombie_Death,

        COUNT,
    }

    public enum Instant_Transition_States
    {
        Jump_Normal_Landing = 0,
        Jump_3m_Prep = 1,
        Hanging_Idle = 2,
        Idle = 4,

        Jump_Normal_Prep = 5,
        Jump_Running = 6,

        Running_Jump = 3,
        Run_Stop_InPlace = 7,

        AirCombo_Smash = 8,

        COUNT,
    }

    public enum Ledge_Trigger_States
    {
        Jump_Running_Fall,

        // normal jump
        Jump_Normal,
        Heroic_Fall,

        // running jump
        Running_Jump,
        Running_Heroic_Fall,
        Jump_Running,

        Fall,
        WallSlide,
        WallJump,

        COUNT,
    }

    public enum Camera_States
    {
        Default,
        Shake,
    }

    public class HashManager : Singleton<HashManager>
    {
        public int[] ArrMainParams = new int[(int)MainParameterType.COUNT];
        public int[] ArrCameraParams = new int[(int)CameraTrigger.COUNT];
        public int[] ArrAITransitionParams = new int[(int)AI_Transition.COUNT];
        public int[] ArrAIStateNames = new int[(int)AI_State_Name.COUNT];

        public Dictionary<Hit_Reaction_States, int> DicHitReactionStates =
            new Dictionary<Hit_Reaction_States, int>();

        public int[] ArrInstantTransitionStates = new int[(int)Instant_Transition_States.COUNT];
        public int[] ArrLedgeTriggerStates = new int[(int)Ledge_Trigger_States.COUNT];

        public Dictionary<Camera_States, int> DicCameraStates =
            new Dictionary<Camera_States, int>();

        private void Awake()
        {
            // animation transitions
            for (int i = 0; i < (int)MainParameterType.COUNT; i++)
            {
                ArrMainParams[i] = Animator.StringToHash(((MainParameterType)i).ToString());
            }

            // camera transitions
            for (int i = 0; i < (int)CameraTrigger.COUNT; i++)
            {
                ArrCameraParams[i] = Animator.StringToHash(((CameraTrigger)i).ToString());
            }

            // ai transitions
            for (int i = 0; i < (int)AI_Transition.COUNT; i++)
            {
                ArrAITransitionParams[i] = Animator.StringToHash(((AI_Transition)i).ToString());
            }

            // ai states
            for (int i = 0; i < (int)AI_State_Name.COUNT; i++)
            {
                ArrAIStateNames[i] = Animator.StringToHash(((AI_State_Name)i).ToString());
            }

            // hit reaction states
            Hit_Reaction_States[] arrHitReactionStates = System.Enum.GetValues(typeof(Hit_Reaction_States))
                as Hit_Reaction_States[];

            foreach(Hit_Reaction_States t in arrHitReactionStates)
            {
                DicHitReactionStates.Add(t, Animator.StringToHash(t.ToString()));
            }

            // instant transition states
            for (int i = 0; i < ArrInstantTransitionStates.Length; i++)
            {
                ArrInstantTransitionStates[i] = Animator.StringToHash(((Instant_Transition_States)i).ToString());
            }

            // ledge trigger states
            for (int i = 0; i < ArrLedgeTriggerStates.Length; i++)
            {
                ArrLedgeTriggerStates[i] = Animator.StringToHash(((Ledge_Trigger_States)i).ToString());
            }

            // camera states
            Camera_States[] arrCameraStates = System.Enum.GetValues(typeof(Camera_States))
                as Camera_States[];

            foreach(Camera_States t in arrCameraStates)
            {
                DicCameraStates.Add(t, Animator.StringToHash(t.ToString()));
            }
        }
    }
}