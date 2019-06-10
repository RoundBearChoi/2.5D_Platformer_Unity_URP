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

        public GameObject StartSphere;
        public GameObject EndSphere;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void GoToTarget()
        {
            navMeshAgent.enabled = true;
            StartSphere.transform.parent = null;
            EndSphere.transform.parent = null;

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
                    StartSphere.transform.position = transform.position;
                    navMeshAgent.CompleteOffMeshLink();

                    yield return new WaitForEndOfFrame();
                    EndPosition = transform.position;
                    EndSphere.transform.position = transform.position;
                    navMeshAgent.isStopped = true;
                    yield break;
                }

                Vector3 dist = transform.position - navMeshAgent.destination;
                if (Vector3.SqrMagnitude(dist) < 0.5f)
                {
                    StartPosition = transform.position;
                    EndPosition = transform.position;
                    navMeshAgent.isStopped = true;
                    yield break;
                }

                yield return new WaitForEndOfFrame();
            }
        }
    }
}


