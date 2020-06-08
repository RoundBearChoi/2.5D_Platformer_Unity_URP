using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class CollisionSphereData
    {
        public List<GameObject> BottomSpheres;
        public List<GameObject> FrontSpheres;
        public List<GameObject> BackSpheres;
        public List<GameObject> UpSpheres;

        public List<OverlapChecker> FrontOverlapCheckers;
        public List<OverlapChecker> AllOverlapCheckers;

        public delegate void DoSomething();

        public DoSomething Reposition_FrontSpheres;
        public DoSomething Reposition_BottomSpheres;
        public DoSomething Reposition_BackSpheres;
        public DoSomething Reposition_UpSpheres;
    }
}