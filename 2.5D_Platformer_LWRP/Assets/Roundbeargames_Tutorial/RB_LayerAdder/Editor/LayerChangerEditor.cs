using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Roundbeargames
{
    [CustomEditor(typeof(LayerChanger))]
    public class LayerChangerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Change Layer"))
            {
                LayerChanger layerChanger = (LayerChanger)target;

                Dictionary<string, int> dic = LayerAdderEditor.GetAllLayers();

                layerChanger.ChangeLayer(dic);
            }
        }
    }
}