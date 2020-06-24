using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
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

    public enum Animation_States
    {
        Jump_Normal_Landing,
        Jump_3m_Prep,
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

        public Dictionary<Animation_States, int> DicAnimationStates =
            new Dictionary<Animation_States, int>();

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
            Animation_States[] arrAnimationStates = System.Enum.GetValues(typeof(Animation_States))
                as Animation_States[];

            foreach(Animation_States t in arrAnimationStates)
            {
                DicAnimationStates.Add(t, Animator.StringToHash(t.ToString()));
            }
        }
    }
}