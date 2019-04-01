using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial_1
{
    public abstract class Ability : MonoBehaviour
    {
        public abstract AbilityType abilityType { get; }
        public abstract void UpdateAbility(CharacterStateBase characterStateBase, Animator animator);
    }
}