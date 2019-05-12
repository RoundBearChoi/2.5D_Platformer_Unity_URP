using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class PlayGame : MonoBehaviour
    {
        public CharacterSelect characterSelect;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (characterSelect.SelectedCharacterType != PlayableCharacterType.NONE)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(RBScenes.TutorialScene_Sample.ToString());
                }
                else
                {
                    Debug.Log("must select character first");
                }
            }
        }
    }
}