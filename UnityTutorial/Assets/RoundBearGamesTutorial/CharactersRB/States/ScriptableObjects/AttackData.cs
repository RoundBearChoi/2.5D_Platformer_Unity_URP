using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial_1
{
    [CreateAssetMenu(fileName = "New State", menuName = "RoundBearGames/CharacterState/AttackState")]
    public class AttackData : ScriptableObject
    {
        public float AttackStart;
        public float AttackEnd;
    }
}
