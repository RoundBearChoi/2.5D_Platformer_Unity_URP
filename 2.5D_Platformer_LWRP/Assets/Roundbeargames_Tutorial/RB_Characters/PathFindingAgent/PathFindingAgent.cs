using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    public class PathFindingAgent : MonoBehaviour
    {
        public bool TargetPlayableCharacter;
        public GameObject target;
        NavMeshAgent navMeshAgent;
        Coroutine MoveRoutine;
        public GameObject StartSphere;
        public GameObject EndSphere;
        public bool StartWalk;

        public CharacterControl owner = null;

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
            MoveRoutine = StartCoroutine(_Move());
        }

        private void OnEnable()
        {
            if (MoveRoutine != null)
            {
                StopCoroutine(MoveRoutine);
            }
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
                    break;
                }

                Vector3 dist = transform.position - navMeshAgent.destination;
                if (Vector3.SqrMagnitude(dist) < 0.5f)
                {
                    StartSphere.transform.position = navMeshAgent.destination;
                    EndSphere.transform.position = navMeshAgent.destination;

                    navMeshAgent.isStopped = true;
                    StartWalk = true;
                    break;
                }

                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(0.5f);

            owner.navMeshObstacle.carving = true;
        }
    }
}


