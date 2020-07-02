using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class AnimationData
    {
        public bool InstantTransitionMade;

        public Dictionary<CharacterAbility, int> CurrentRunningAbilities = new Dictionary<CharacterAbility, int>();

        public delegate bool bool_type(System.Type type);

        public bool_type IsRunning;
    }
}