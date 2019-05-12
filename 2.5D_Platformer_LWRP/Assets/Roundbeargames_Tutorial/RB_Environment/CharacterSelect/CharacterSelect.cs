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

    [CreateAssetMenu(fileName = "CharacterSelect", menuName = "Roundbeargames/CharacterSelect/CharacterSelect")]
    public class CharacterSelect : ScriptableObject
    {
        public PlayableCharacterType SelectedCharacterType;
    }
}