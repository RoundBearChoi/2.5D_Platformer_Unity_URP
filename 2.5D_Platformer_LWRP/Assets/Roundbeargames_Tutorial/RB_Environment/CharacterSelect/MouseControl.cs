using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class MouseControl : MonoBehaviour
    {
        Ray ray;
        RaycastHit hit;
        public PlayableCharacterType selectedCharacterType;
        public CharacterSelect characterSelect;
        CharacterSelectLight characterSelectLight;
        CharacterHoverLight characterHoverLight;
        GameObject whiteSelection;
        Animator characterSelectCamAnimator;

        private void Awake()
        {
            characterSelect.SelectedCharacterType = PlayableCharacterType.NONE;
            characterSelectLight = GameObject.FindObjectOfType<CharacterSelectLight>();
            characterHoverLight = GameObject.FindObjectOfType<CharacterHoverLight>();

            whiteSelection = GameObject.Find("WhiteSelection");
            whiteSelection.SetActive(false);

            characterSelectCamAnimator = GameObject.Find("CharacterSelectCameraController").GetComponent<Animator>();
        }

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
                    characterSelectLight.transform.position = characterHoverLight.transform.position;
                    CharacterControl control = CharacterManager.Instance.GetCharacter(selectedCharacterType);
                    characterSelectLight.transform.parent = control.SkinnedMeshAnimator.transform;
                    characterSelectLight.light.enabled = true;

                    whiteSelection.SetActive(true);
                    whiteSelection.transform.parent = control.SkinnedMeshAnimator.transform;
                    whiteSelection.transform.localPosition = new Vector3(0f, -0.045f, 0f);
                }
                else
                {
                    characterSelect.SelectedCharacterType = PlayableCharacterType.NONE;
                    characterSelectLight.light.enabled = false;
                    whiteSelection.SetActive(false);
                }

                foreach(CharacterControl c in CharacterManager.Instance.Characters)
                {
                    if (c.playableCharacterType == selectedCharacterType)
                    {
                        c.SkinnedMeshAnimator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.ClickAnimation], true);
                    }
                    else
                    {
                        c.SkinnedMeshAnimator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.ClickAnimation], false);
                    }
                }

                characterSelectCamAnimator.SetBool(selectedCharacterType.ToString(), true);
            }
        }
    }
}