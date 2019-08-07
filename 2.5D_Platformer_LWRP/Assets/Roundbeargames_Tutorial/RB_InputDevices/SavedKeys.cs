using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Roundbeargames/Settings/SavedKeys")]
    public class SavedKeys : ScriptableObject
    {
        public List<KeyCode> KeyCodesList = new List<KeyCode>();
    }
}