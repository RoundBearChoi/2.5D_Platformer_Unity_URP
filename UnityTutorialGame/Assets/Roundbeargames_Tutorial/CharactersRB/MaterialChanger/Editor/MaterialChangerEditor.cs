using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RoundBearGames_ObstacleCourse
{
    [CustomEditor(typeof(MaterialChanger))]
    public class MaterialChangerEditor : Editor
    {
        SerializedProperty material;
        SerializedProperty CurrentMaterials;
        SerializedProperty NewMaterials;

        private void OnEnable()
        {
            material = serializedObject.FindProperty("material");
            CurrentMaterials = serializedObject.FindProperty("CurrentMaterials");
            NewMaterials = serializedObject.FindProperty("NewMaterials");
        }

        public override void OnInspectorGUI()
        {
            //DrawDefaultInspector();
            serializedObject.Update();

            MaterialChanger changer = (MaterialChanger)target;

            //Simple Material Changer

            EditorGUILayout.Space();

            GUILayout.BeginVertical("box");

            EditorGUILayout.Space();

            EditorGUILayout.HelpBox("Use this if character only has 1 material", MessageType.Info);

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(material);

            EditorGUILayout.Space();

            if (GUILayout.Button("Change Material"))
            {
                changer.ChangeMaterial();
            }

            EditorGUILayout.Space();

            GUILayout.EndVertical();

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            // Complext Material Changer

            EditorGUILayout.Space();

            GUILayout.BeginVertical("box");

            EditorGUILayout.Space();

            EditorGUILayout.HelpBox("Use this if character has multiple materials", MessageType.Info);

            EditorGUILayout.Space();

            if (GUILayout.Button("Identify Current Materials"))
            {
                if (CurrentMaterials.arraySize != 0 || NewMaterials.arraySize != 0)
                {
                    CurrentMaterials.ClearArray();
                    NewMaterials.ClearArray();
                }
                else
                {
                    changer.IdentifyMaterials();
                }
            }

            EditorGUILayout.Space();

            GUILayout.BeginHorizontal("box");

            GUILayout.BeginVertical("box");
            foreach(SerializedProperty s in CurrentMaterials)
            {
                EditorGUILayout.PropertyField(s);
            }
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box");
            foreach (SerializedProperty s in NewMaterials)
            {
                EditorGUILayout.PropertyField(s);
            }
            GUILayout.EndVertical();


            GUILayout.EndHorizontal();

            EditorGUILayout.Space();

            if (GUILayout.Button("Change Complex Material"))
            {
                if (CurrentMaterials.arraySize != NewMaterials.arraySize)
                {
                    CurrentMaterials.ClearArray();
                    NewMaterials.ClearArray();
                }
                else if (CurrentMaterials.arraySize == 0)
                {
                    Debug.Log("List is empty");
                }
                else
                {
                    changer.ChangeComplexMaterial();
                }
            }

            EditorGUILayout.Space();

            GUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }
    }
}