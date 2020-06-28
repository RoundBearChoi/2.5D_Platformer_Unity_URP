using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class AnimationData
    {
        public bool InstantTransitionMade;

        public Dictionary<StateData, int> CurrentRunningAbilities = new Dictionary<StateData, int>();

        public delegate bool bool_type(System.Type type);

        public bool_type IsRunning;
    }
}