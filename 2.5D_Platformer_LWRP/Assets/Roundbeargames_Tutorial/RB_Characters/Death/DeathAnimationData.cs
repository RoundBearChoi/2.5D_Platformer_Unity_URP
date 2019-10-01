using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum DeathType
    {
        NONE,
        LAUNCH_INTO_AIR,
        GROUND_SHOCK,
    }

    [CreateAssetMenu(fileName = "New ScriptableObject", menuName = "Roundbeargames/Death/DeathAnimationData")]
    public class DeathAnimationData : ScriptableObject
    {
        public RuntimeAnimatorController Animator;
        public DeathType deathType;
        public bool IsFacingAttacker;
    }
}

