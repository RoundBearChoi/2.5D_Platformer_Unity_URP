using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public enum PlayableCharacterType
    {
        NONE,
        YELLOW,
        RED,
        GREEN,
    }

    [CreateAssetMenu(fileName = "CharacterHover", menuName = "Roundbeargames/CharacterSelect/CharacterHover")]
    public class CharacterHover : ScriptableObject
    {
        public PlayableCharacterType HoveringCharacterType;
    }
}