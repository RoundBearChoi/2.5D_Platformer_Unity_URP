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

        public ReturnBool EarlyTurnIsLocked;
    }
}