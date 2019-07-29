using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Roundbeargames/Settings/FrameSettings")]
    public class FrameSettings : ScriptableObject
    {
        [Range(0.01f, 1f)]
        public float TimeScale;
        public int TargetFPS;
    }
}