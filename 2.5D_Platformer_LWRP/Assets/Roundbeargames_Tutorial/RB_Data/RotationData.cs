using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class RotationData
    {
        public bool LockTurn;
        public float UnlockTiming;

        public delegate bool ReturnBool();
        public delegate void DoSomething(bool faceForward);

        public ReturnBool IsFacingForward;
        public DoSomething FaceForward;
    }
}