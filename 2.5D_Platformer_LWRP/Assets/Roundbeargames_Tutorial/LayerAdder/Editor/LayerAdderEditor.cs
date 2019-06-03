using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace roundbeargames_tutorial
{
    [CustomEditor(typeof(LayerAdder))]
    public class LayerAdderEditor: Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            //var style = new GUIStyle(GUI.skin.button);
            //style.normal.textColor = Color.red;

            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Add RB Default Layers"))
            {
                RB_Layers[] arr = System.Enum.GetValues(typeof(RB_Layers)) as RB_Layers[];

                foreach(RB_Layers r in arr)
                {
                    Debug.Log("creating layer: " + r.ToString());
                    CreateLayer(r.ToString());
                }
            }

            if (GUILayout.Button("Set Default Layer Collisions"))
            {
                Dictionary<string, int> dic = GetAllLayers();

                foreach (KeyValuePair<string, int> d1 in dic)
                {
                    foreach (KeyValuePair<string, int> d2 in dic)
                    {
                        Physics.IgnoreLayerCollision(d1.Value, d2.Value, true);
                    }
                }

                Physics.IgnoreLayerCollision(dic["Default"], dic["Default"], false);

                Debug.Log("default collisions set");
            }

            EditorGUILayout.Space();

            GUI.backgroundColor = Color.white;
            if (GUILayout.Button("Uncheck All Layer Collisions"))
            {
                Dictionary<string, int> dic = GetAllLayers();

                foreach(KeyValuePair<string, int> d1 in dic)
                {
                    foreach (KeyValuePair<string, int> d2 in dic)
                    {
                        Physics.IgnoreLayerCollision(d1.Value, d2.Value, true);
                    }
                }

                Debug.Log("all collisions unchecked");
            }

            if (GUILayout.Button("Check All Layer Collisions"))
            {
                Dictionary<string, int> dic = GetAllLayers();

                foreach (KeyValuePair<string, int> d1 in dic)
                {
                    foreach (KeyValuePair<string, int> d2 in dic)
                    {
                        Physics.IgnoreLayerCollision(d1.Value, d2.Value, false);
                    }
                }

                Debug.Log("all collisions checked");
            }
        }

        Dictionary<string, int> GetAllLayers()
        {
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty layers = tagManager.FindProperty("layers");
            int layerSize = layers.arraySize;

            Dictionary<string, int> LayerDictionary = new Dictionary<string, int>();

            for (int i = 0; i < layerSize; i++)
            {
                SerializedProperty element = layers.GetArrayElementAtIndex(i);
                string layerName = element.stringValue;

                if (!string.IsNullOrEmpty(layerName))
                {
                    LayerDictionary.Add(layerName, i);
                }
            }

            return LayerDictionary;
        }

        /// <summary>
        /// Create a layer at the next available index. Returns silently if layer already exists.
        /// </summary>
        /// <param name="name">Name of the layer to create</param>
        public static void CreateLayer(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new System.ArgumentNullException("name", "New layer name string is either null or empty.");

            var tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            var layerProps = tagManager.FindProperty("layers");
            var propCount = layerProps.arraySize;

            SerializedProperty firstEmptyProp = null;

            for (var i = 0; i < propCount; i++)
            {
                var layerProp = layerProps.GetArrayElementAtIndex(i);

                var stringValue = layerProp.stringValue;

                if (stringValue == name) return;

                if (i < 8 || stringValue != string.Empty) continue;

                if (firstEmptyProp == null)
                    firstEmptyProp = layerProp;
            }

            if (firstEmptyProp == null)
            {
                UnityEngine.Debug.LogError("Maximum limit of " + propCount + " layers exceeded. Layer \"" + name + "\" not created.");
                return;
            }

            firstEmptyProp.stringValue = name;
            tagManager.ApplyModifiedProperties();
        }
    }
}