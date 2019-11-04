using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class HashManager : Singleton<HashManager>
    {
        public Dictionary<TransitionParameter, int> DicMainParams =
            new Dictionary<TransitionParameter, int>();

        public Dictionary<CameraTrigger, int> DicCameraTriggers =
            new Dictionary<CameraTrigger, int>();

        public Dictionary<AI_Walk_Transitions, int> DicAITrans =
            new Dictionary<AI_Walk_Transitions, int>();

        private void Awake()
        {
            //animation transitions
            TransitionParameter[] arrParams = System.Enum.GetValues(typeof(TransitionParameter))
                as TransitionParameter[];
                
            foreach(TransitionParameter t in arrParams)
            {
                DicMainParams.Add(t, Animator.StringToHash(t.ToString()));
            }

            //camera transitions
            CameraTrigger[] arrCamTrans = System.Enum.GetValues(typeof(CameraTrigger))
                as CameraTrigger[];

            foreach (CameraTrigger t in arrCamTrans)
            {
                DicCameraTriggers.Add(t, Animator.StringToHash(t.ToString()));
            }

            //ai transitions
            AI_Walk_Transitions[] arrAITrans = System.Enum.GetValues(typeof(AI_Walk_Transitions))
                as AI_Walk_Transitions[];

            foreach (AI_Walk_Transitions t in arrAITrans)
            {
                DicAITrans.Add(t, Animator.StringToHash(t.ToString()));
            }
        }
    }
}