using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Roundbeargames
{
    [CustomEditor(typeof(LayerAdder))]
    public class LayerAdderEditor: Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Add RB Default Layers"))
            {
                RB_Layers[] arr = System.Enum.GetValues(typeof(RB_Layers)) as RB_Layers[];

                foreach(RB_Layers r in arr)
                {
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
                Physics.IgnoreLayerCollision(dic[RB_Layers.CHARACTER.ToString()], dic["Default"], false);
                Physics.IgnoreLayerCollision(dic[RB_Layers.DEADBODY.ToString()], dic["Default"], false);

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

        public static Dictionary<string, int> GetAllLayers()
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

        void CreateLayer(string name)
        {
            bool Success = false;
            Dictionary<string, int> dic = GetAllLayers();

            if (!dic.ContainsKey(name))
            {
                SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
                SerializedProperty layers = tagManager.FindProperty("layers");

                for (int i = 0; i < 31; i++)
                {
                    SerializedProperty element = layers.GetArrayElementAtIndex(i);
                    if (string.IsNullOrEmpty(element.stringValue) && i >= 8)
                    {
                        element.stringValue = name;

                        tagManager.ApplyModifiedProperties(); //save changes
                        Success = true;
                        Debug.Log(i.ToString() + " layer created: " + name);
                        break;
                    }
                }

                if (!Success)
                {
                    Debug.Log("could not create layer");
                }
            }
            else
            {
                Debug.Log("layer already exists: " + name);
            }
        }
    }
}