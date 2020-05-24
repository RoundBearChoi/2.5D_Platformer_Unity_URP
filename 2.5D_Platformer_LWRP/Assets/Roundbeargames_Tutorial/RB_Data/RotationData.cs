using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class RotationData
    {
        public bool LockEarlyTurn;
        public bool LockDirectionNextState;

        public delegate bool ReturnBool();
        public delegate void DoSomething(bool faceForward);

        public ReturnBool EarlyTurnIsLocked;
        public ReturnBool IsFacingForward;
        public DoSomething FaceForward;
    }
}