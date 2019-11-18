using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum AI_TYPE
    {
        NONE,
        WALK_AND_JUMP,
    }


    public class AIController : MonoBehaviour
    {
        public AI_TYPE InitialAI;

        List<AISubset> AIList = new List<AISubset>();
        Coroutine AIRoutine;
        Vector3 TargetDir = new Vector3();
        CharacterControl control;

        private void Awake()
        {
            control = this.gameObject.GetComponentInParent<CharacterControl>();
        }

        private void Start()
        {
            InitializeAI();
        }

        public void InitializeAI()
        {
            if (AIList.Count == 0)
            {
                AISubset[] arr = this.gameObject.GetComponentsInChildren<AISubset>();

                foreach (AISubset s in arr)
                {
                    if (!AIList.Contains(s))
                    {
                        AIList.Add(s);
                        s.gameObject.SetActive(false);
                    }
                }
            }

            AIRoutine = StartCoroutine(_InitAI());
        }

        private void OnEnable()
        {
            if (AIRoutine != null)
            {
                StopCoroutine(AIRoutine);
            }
        }

        private IEnumerator _InitAI()
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

        public void WalkStraightToStartSphere()
        {
            TargetDir = control.aiProgress.pathfindingAgent.
                StartSphere.transform.position -
                control.transform.position;

            if (TargetDir.z > 0f)
            {
                control.MoveRight = true;
                control.MoveLeft = false;
            }
            else
            {
                control.MoveRight = false;
                control.MoveLeft = true;
            }
        }

        public void WalkStraightToEndSphere()
        {
            TargetDir = control.aiProgress.pathfindingAgent.
                EndSphere.transform.position -
                control.transform.position;

            if (TargetDir.z > 0f)
            {
                control.MoveRight = true;
                control.MoveLeft = false;
            }
            else
            {
                control.MoveRight = false;
                control.MoveLeft = true;
            }
        }
    }
}