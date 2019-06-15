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

        List<Coroutine> MoveRoutines = new List<Coroutine>();

        public GameObject StartSphere;
        public GameObject EndSphere;
        public bool StartWalk;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void GoToTarget()
        {
            navMeshAgent.enabled = true;
            StartSphere.transform.parent = null;
            EndSphere.transform.parent = null;
            StartWalk = false;

            navMeshAgent.isStopped = false;

            if (TargetPlayableCharacter)
            {
                target = CharacterManager.Instance.GetPlayableCharacter().gameObject;
            }

            navMeshAgent.SetDestination(target.transform.position);

            if (MoveRoutines.Count != 0)
            {
                if (MoveRoutines[0] != null)
                {
                    StopCoroutine(MoveRoutines[0]);
                }
                
                MoveRoutines.RemoveAt(0);
            }

            MoveRoutines.Add(StartCoroutine(_Move()));
        }

        IEnumerator _Move()
        {
            while (true)
            {
                if (navMeshAgent.isOnOffMeshLink)
                {
                    StartSphere.transform.position = navMeshAgent.currentOffMeshLinkData.startPos;
                    EndSphere.transform.position = navMeshAgent.currentOffMeshLinkData.endPos;

                    navMeshAgent.CompleteOffMeshLink();
                    
                    navMeshAgent.isStopped = true;
                    StartWalk = true;
                    yield break;
                }

                Vector3 dist = transform.position - navMeshAgent.destination;
                if (Vector3.SqrMagnitude(dist) < 0.5f)
                {
                    StartSphere.transform.position = navMeshAgent.destination;
                    EndSphere.transform.position = navMeshAgent.destination;

                    navMeshAgent.isStopped = true;
                    StartWalk = true;
                    yield break;
                }

                yield return new WaitForEndOfFrame();
            }
        }
    }
}


