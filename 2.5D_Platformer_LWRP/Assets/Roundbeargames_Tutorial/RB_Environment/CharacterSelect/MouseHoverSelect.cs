using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class MouseHoverSelect : MonoBehaviour
    {
        Ray ray;
        RaycastHit hit;
        public PlayableCharacterType selectedCharacterType;
        public CharacterSelect characterSelect;

        void Update()
        {
            ray = CameraManager.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                CharacterControl control = hit.collider.gameObject.GetComponent<CharacterControl>();
                if (control != null)
                {
                    selectedCharacterType = control.playableCharacterType;
                }
                else
                {
                    selectedCharacterType = PlayableCharacterType.NONE;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (selectedCharacterType != PlayableCharacterType.NONE)
                {
                    characterSelect.SelectedCharacterType = selectedCharacterType;
                }
            }
        }
    }
}