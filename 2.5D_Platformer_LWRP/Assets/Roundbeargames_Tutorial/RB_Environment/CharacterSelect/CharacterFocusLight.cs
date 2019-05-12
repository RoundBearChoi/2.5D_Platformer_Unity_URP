using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class CharacterFocusLight : MonoBehaviour
    {
        public CharacterControl HoverSelectedCharacter;
        public MouseHoverSelect mouseHoverSelect;
        public Vector3 Offset = new Vector3();
        Vector3 TargetPos = new Vector3();
        Light light;

        private void Start()
        {
            mouseHoverSelect = GameObject.FindObjectOfType<MouseHoverSelect>();
            light = GetComponent<Light>();
        }

        private void Update()
        {
            if (mouseHoverSelect.selectedCharacterType == PlayableCharacterType.NONE)
            {
                HoverSelectedCharacter = null;
                light.enabled = false;
            }
            else
            {
                light.enabled = true;
                LightUpSelectedCharacter();
            }
        }

        private void LightUpSelectedCharacter()
        {
            if (HoverSelectedCharacter == null)
            {
                HoverSelectedCharacter = CharacterManager.Instance.GetCharacter(mouseHoverSelect.selectedCharacterType);
                this.transform.position = HoverSelectedCharacter.transform.position + HoverSelectedCharacter.transform.TransformDirection(Offset);
            }
        }
    }
}