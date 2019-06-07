using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace roundbeargames_tutorial
{
    public class PathFindingAgent : MonoBehaviour
    {
        public bool TargetPlayableCharacter;
        public GameObject target;
        NavMeshAgent navMeshAgent;

        public Vector3 StartPosition;
        public Vector3 EndPosition;
        Coroutine Move;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void GoToTarget()
        {
            navMeshAgent.isStopped = false;

            if (TargetPlayableCharacter)
            {
                target = CharacterManager.Instance.GetPlayableCharacter().gameObject;
            }

            navMeshAgent.SetDestination(target.transform.position);

            if (Move != null)
            {
                StopCoroutine(Move);
            }

            Move = StartCoroutine(_Move());
        }

        IEnumerator _Move()
        {
            while (true)
            {
                if (navMeshAgent.isOnOffMeshLink)
                {
                    StartPosition = transform.position;
                    navMeshAgent.CompleteOffMeshLink();

                    yield return new WaitForEndOfFrame();
                    EndPosition = transform.position;
                    navMeshAgent.isStopped = true;
                    yield break;
                }

                yield return new WaitForEndOfFrame();
            }
        }
    }
}


