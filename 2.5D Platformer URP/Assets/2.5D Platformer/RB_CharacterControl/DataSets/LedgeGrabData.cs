using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class LedgeGrabData
    {
        public bool isGrabbingLedge;

        public delegate void DoSomething();

        public DoSomething LedgeCollidersOff;
    }
}