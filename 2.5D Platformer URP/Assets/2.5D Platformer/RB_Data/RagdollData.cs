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
        public FlyingRagdollData flyingRagdollData;

        public delegate Collider GetCollider(string name);
        public delegate void ProcRagdoll(RagdollPushType type);
        public delegate void DoSomething();


        public GetCollider GetBody;
        public ProcRagdoll AddForceToDamagedPart;
        public DoSomething ClearExistingVelocity;
    }

    [System.Serializable]
    public class FlyingRagdollData
    {
        public bool IsTriggered = false;
        public CharacterControl Attacker = null;
    }
}