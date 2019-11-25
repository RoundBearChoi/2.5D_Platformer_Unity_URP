using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Roundbeargames
{
    [CustomEditor(typeof(ColliderRemover))]
    public class ColliderRemoverEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Remove All Colliders"))
            {
                ColliderRemover rem = target as ColliderRemover;
                rem.RemoveAllColliders();
            }
        }
    }
}