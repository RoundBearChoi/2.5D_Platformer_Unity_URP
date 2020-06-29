using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
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

        public CharacterControl GetCharacter(Animator animator)
        {
            foreach (CharacterControl control in Characters)
            {
                if (control.SkinnedMeshAnimator == animator)
                {
                    return control;
                }
            }

            return null;
        }

        public CharacterControl GetCharacter(GameObject obj)
        {
            foreach (CharacterControl control in Characters)
            {
                if (control.gameObject == obj)
                {
                    return control;
                }
            }

            return null;
        }

        public CharacterControl GetPlayableCharacter()
        {
            foreach (CharacterControl control in Characters)
            {
                if (control.subComponentProcessor.ArrSubComponents[(int)SubComponentType.MANUALINPUT] != null)
                {
                    return control;
                }
                //if (control.subComponentProcessor.ComponentsDic.ContainsKey(SubComponentType.MANUALINPUT))
                //{
                //    return control;
                //}
            }

            return null;
        }
    }
}