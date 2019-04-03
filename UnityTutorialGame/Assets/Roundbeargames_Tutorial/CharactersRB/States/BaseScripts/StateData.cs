using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public abstract class StateData : ScriptableObject
    {
        public float Duration;

        public abstract void UpdateAbility(CharacterState characterState, Animator animator);
    }
}