using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum AI_TYPE
    {
        WALK_AND_JUMP,
        RUN,
    }


    public class AIController : MonoBehaviour
    {
        public List<AISubset> AIList = new List<AISubset>();
        public AI_TYPE InitialAI;

        public void Awake()
        {
            AISubset[] arr = this.gameObject.GetComponentsInChildren<AISubset>();

            foreach(AISubset s in arr)
            {
                if (!AIList.Contains(s))
                {
                    AIList.Add(s);
                    s.gameObject.SetActive(false);
                }
            }
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();

            TriggerAI(InitialAI);
        }

        public void TriggerAI(AI_TYPE aiType)
        {
            AISubset next = null;

            foreach(AISubset s in AIList)
            {
                s.gameObject.SetActive(false);

                if (s.aiType == aiType)
                {
                    next = s;
                }
            }

            if (next != null)
            {
                next.gameObject.SetActive(true);
            }
        }
    }
}