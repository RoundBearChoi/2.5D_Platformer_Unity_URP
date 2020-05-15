using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class RagdollData
    {
        public bool RagdollTriggered;
        public List<Collider> BodyParts;

        public delegate Collider GetCollider(string name);
        public GetCollider GetBody;
    }
}