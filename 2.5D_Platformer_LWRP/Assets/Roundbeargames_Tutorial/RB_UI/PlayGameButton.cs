using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class PlayGameButton : MonoBehaviour
    {
        public void OnClick_PlayGame()
        {
            //Debug.Log("clicked: PlayGame");
            UnityEngine.SceneManagement.SceneManager.LoadScene("TutorialScene_Sample_Day");
        }
    }
}