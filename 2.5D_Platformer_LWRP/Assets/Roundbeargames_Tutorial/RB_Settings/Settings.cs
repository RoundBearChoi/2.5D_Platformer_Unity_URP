using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class Settings : MonoBehaviour
    {
        public FrameSettings frameSettings;

        private void Awake()
        {
            Debug.Log("timeScale: " + frameSettings.TimeScale);
            Time.timeScale = frameSettings.TimeScale;

            Debug.Log("targetFrameRate: " + frameSettings.TargetFPS);
            Application.targetFrameRate = frameSettings.TargetFPS;
        }
    }
}
