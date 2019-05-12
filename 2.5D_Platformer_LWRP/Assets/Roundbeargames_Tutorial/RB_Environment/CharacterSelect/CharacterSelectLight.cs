using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class CharacterSelectLight : MonoBehaviour
    {
        public PlayableCharacterType HighlightedCharacter;
        //public CharacterHoverLight hoverLight;

        public Light light;

        private void Start()
        {
            //hoverLight = GameObject.FindObjectOfType<CharacterHoverLight>();
            light = GetComponent<Light>();
            light.enabled = false;
        }
    }
}