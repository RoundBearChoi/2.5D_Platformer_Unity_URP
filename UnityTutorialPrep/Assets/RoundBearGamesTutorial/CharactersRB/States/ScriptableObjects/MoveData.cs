using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoundBearGames_ObstacleCourse
{
    [CreateAssetMenu(fileName = "New State", menuName = "RoundBearGames/CharacterState/MoveState")]
    public class MoveData : ScriptableObject
    {
        public float WalkSpeed;
        public float JumpForce;
    }
}
