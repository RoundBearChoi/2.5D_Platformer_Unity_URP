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
        AI_Attack,
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
        }
    }
}