using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class CharacterManager : Singleton<CharacterManager>
    {
        public List<CharacterControl> Characters = new List<CharacterControl>();

        public CharacterControl GetCharacter(PlayableCharacterType playableCharacterType)
        {
            foreach(CharacterControl control in Characters)
            {
                if (control.playableCharacterType == playableCharacterType)
                {
                    return control;
                }
            }

            return null;
        }
    }
}