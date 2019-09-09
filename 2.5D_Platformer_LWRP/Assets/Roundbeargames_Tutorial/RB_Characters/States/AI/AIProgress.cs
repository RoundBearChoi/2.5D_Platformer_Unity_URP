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

        public float GetDistanceToDestination()
        {
            return Vector3.SqrMagnitude(
                control.aiProgress.pathfindingAgent.StartSphere.transform.position
                - control.transform.position);
        }
    }
}

