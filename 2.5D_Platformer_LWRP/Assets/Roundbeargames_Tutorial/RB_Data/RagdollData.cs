using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class RagdollData
    {
        public bool RagdollTriggered;
        public Collider[] ArrBodyParts;
        //public List<Collider> BodyParts;
        public bool FlyingRagdoll;

        public delegate Collider GetCollider(string name);
        public delegate void DoSomething(bool boolData);

        public GetCollider GetBody;
        public DoSomething AddForceToDamagedPart;
    }
}