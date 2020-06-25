using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum CameraTrigger
    {
        Default,
        Shake,
    }

    public enum AI_Transitions
    {
        start_walking,
        jump_platform,
        fall_platform,
    }

    public enum AI_States
    {
        SendPathfindingAgent,
        AI_Attack,
    }

    public enum Hit_Reaction_States
    {
        Head_Hit,
        Zombie_Death,

        COUNT,
    }

    public enum Instant_Transition_States
    {
        Jump_Normal_Landing,
        Jump_3m_Prep,
        Hanging_Idle,
        Running_Jump,
        Idle,

        Jump_Normal_Prep,
        Jump_Running,
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
    }

    public enum Camera_States
    {
        Default,
        Shake,
    }

    public class HashManager : Singleton<HashManager>
    {
        public Dictionary<TransitionParameter, int> DicMainParams =
            new Dictionary<TransitionParameter, int>();

        public Dictionary<CameraTrigger, int> DicCameraTriggers =
            new Dictionary<CameraTrigger, int>();

        public Dictionary<AI_Transitions, int> DicAITrans =
            new Dictionary<AI_Transitions, int>();

        public Dictionary<AI_States, int> DicAIStates =
            new Dictionary<AI_States, int>();

        public Dictionary<Hit_Reaction_States, int> DicHitReactionStates =
            new Dictionary<Hit_Reaction_States, int>();

        public Dictionary<Instant_Transition_States, int> DicInstantTransitionStates =
            new Dictionary<Instant_Transition_States, int>();

        public Dictionary<Ledge_Trigger_States, int> DicLedgeTriggerStates =
            new Dictionary<Ledge_Trigger_States, int>();

        public Dictionary<Camera_States, int> DicCameraStates =
            new Dictionary<Camera_States, int>();

        private void Awake()
        {
            // animation transitions
            TransitionParameter[] arrParams = System.Enum.GetValues(typeof(TransitionParameter))
                as TransitionParameter[];
                
            foreach(TransitionParameter t in arrParams)
            {
                DicMainParams.Add(t, Animator.StringToHash(t.ToString()));
            }

            // camera transitions
            CameraTrigger[] arrCamTrans = System.Enum.GetValues(typeof(CameraTrigger))
                as CameraTrigger[];

            foreach (CameraTrigger t in arrCamTrans)
            {
                DicCameraTriggers.Add(t, Animator.StringToHash(t.ToString()));
            }

            // ai transitions
            AI_Transitions[] arrAITrans = System.Enum.GetValues(typeof(AI_Transitions))
                as AI_Transitions[];

            foreach (AI_Transitions t in arrAITrans)
            {
                DicAITrans.Add(t, Animator.StringToHash(t.ToString()));
            }

            // ai states
            AI_States[] arrAIStates = System.Enum.GetValues(typeof(AI_States))
                as AI_States[];

            foreach(AI_States t in arrAIStates)
            {
                DicAIStates.Add(t, Animator.StringToHash(t.ToString()));
            }

            // hit reaction states
            Hit_Reaction_States[] arrHitReactionStates = System.Enum.GetValues(typeof(Hit_Reaction_States))
                as Hit_Reaction_States[];

            foreach(Hit_Reaction_States t in arrHitReactionStates)
            {
                DicHitReactionStates.Add(t, Animator.StringToHash(t.ToString()));
            }

            // animation states
            Instant_Transition_States[] arrInstantTransitionStates = System.Enum.GetValues(typeof(Instant_Transition_States))
                as Instant_Transition_States[];

            foreach(Instant_Transition_States t in arrInstantTransitionStates)
            {
                DicInstantTransitionStates.Add(t, Animator.StringToHash(t.ToString()));
            }

            // ledge trigger states
            Ledge_Trigger_States[] arrLedgeTriggerStates = System.Enum.GetValues(typeof(Ledge_Trigger_States))
                as Ledge_Trigger_States[];

            foreach(Ledge_Trigger_States t in arrLedgeTriggerStates)
            {
                DicLedgeTriggerStates.Add(t, Animator.StringToHash(t.ToString()));
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