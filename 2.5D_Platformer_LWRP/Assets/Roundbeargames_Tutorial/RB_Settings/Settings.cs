using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class Settings : MonoBehaviour
    {
        public FrameSettings frameSettings;
        public PhysicsSettings physicsSettings;

        private void Awake()
        {
            //Frames
            Debug.Log("timeScale: " + frameSettings.TimeScale);
            Time.timeScale = frameSettings.TimeScale;

            Debug.Log("targetFrameRate: " + frameSettings.TargetFPS);
            Application.targetFrameRate = frameSettings.TargetFPS;

            //Physics
            Debug.Log("Default Solver Velocity Iterations: " + physicsSettings.DefaultSolverVelocityIterations);
            Physics.defaultSolverVelocityIterations = physicsSettings.DefaultSolverVelocityIterations;
        }
    }
}
