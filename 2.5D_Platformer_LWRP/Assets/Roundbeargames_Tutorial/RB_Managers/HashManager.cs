using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class HashManager : Singleton<HashManager>
    {
        public Dictionary<TransitionParameter, int> DicMainParams = new Dictionary<TransitionParameter, int>();

        private void Awake()
        {
            TransitionParameter[] arr = System.Enum.GetValues(typeof(TransitionParameter)) as TransitionParameter[];
                
            foreach(TransitionParameter t in arr)
            {
                DicMainParams.Add(t, Animator.StringToHash(t.ToString()));
            }
        }
    }
}