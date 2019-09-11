using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class AIProgress : MonoBehaviour
    {
        public PathFindingAgent pathfindingAgent;

        CharacterControl control;

        private void Awake()
        {
            control = this.gameObject.GetComponentInParent<CharacterControl>();
        }

        public float GetDistanceToStartSphere()
        {
            return Vector3.SqrMagnitude(
                control.aiProgress.pathfindingAgent.StartSphere.transform.position
                - control.transform.position);
        }

        public bool EndSphereIsHigher()
        {
            if (EndSphereIsStraight())
            {
                return false;
            }

            if (pathfindingAgent.EndSphere.transform.position.y -
                pathfindingAgent.StartSphere.transform.position.y > 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EndSphereIsLower()
        {
            if (EndSphereIsStraight())
            {
                return false;
            }

            if (pathfindingAgent.EndSphere.transform.position.y -
                pathfindingAgent.StartSphere.transform.position.y > 0f)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool EndSphereIsStraight()
        {
            if (Mathf.Abs(pathfindingAgent.EndSphere.transform.position.y -
                pathfindingAgent.StartSphere.transform.position.y) > 0.01f)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

