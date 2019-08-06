using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Roundbeargames/Settings/PhysicsSettings")]
    public class PhysicsSettings : ScriptableObject
    {
        public int DefaultSolverVelocityIterations;
    }
}