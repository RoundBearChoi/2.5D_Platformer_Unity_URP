using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Roundbeargames
{
    [CustomEditor(typeof(AbilitySearch))]
    public class AbilitySearchEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            AbilitySearch searcher = (AbilitySearch)target;

            if (GUILayout.Button("Search Ability"))
            {
                searcher.Search();
            }
        }
    }
}