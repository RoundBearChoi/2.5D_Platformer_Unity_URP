using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CharacterHoverLight : MonoBehaviour
    {
        public Vector3 Offset = new Vector3();

        CharacterControl HoverSelectedCharacter;
        MouseControl mouseHoverSelect;
        Vector3 TargetPos = new Vector3();
        Light light;

        private void Start()
        {
            mouseHoverSelect = GameObject.FindObjectOfType<MouseControl>();
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
                this.transform.position = HoverSelectedCharacter.SkinnedMeshAnimator.transform.position + HoverSelectedCharacter.transform.TransformDirection(Offset);
                this.transform.parent = HoverSelectedCharacter.SkinnedMeshAnimator.transform;
            }
        }
    }
}