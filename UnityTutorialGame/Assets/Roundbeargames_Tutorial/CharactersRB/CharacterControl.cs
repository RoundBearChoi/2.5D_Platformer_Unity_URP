using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public enum TransitionParameter
    {
        Move,
        Jump,
        ForceTransition,
    }

    public class CharacterControl : MonoBehaviour
    {
        public float Speed;
        public Animator animator;
        public bool MoveRight;
        public bool MoveLeft;
        public bool Jump;
    }
}