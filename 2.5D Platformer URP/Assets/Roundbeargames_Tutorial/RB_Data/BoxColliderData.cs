using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class BoxColliderData
    {
        public bool IsUpdatingSpheres;
        public bool IsLanding;

        public float Size_Update_Speed;
        public float Center_Update_Speed;

        public Vector3 TargetSize;
        public Vector3 TargetCenter;
        public Vector3 LandingPosition;
    }
}