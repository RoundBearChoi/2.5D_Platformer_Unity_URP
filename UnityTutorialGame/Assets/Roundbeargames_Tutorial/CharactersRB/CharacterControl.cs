using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public enum TransitionParameter
    {
        Move,
    }

    public class CharacterControl : MonoBehaviour
    {
        public float Speed;
        public Animator animator;
    }
}