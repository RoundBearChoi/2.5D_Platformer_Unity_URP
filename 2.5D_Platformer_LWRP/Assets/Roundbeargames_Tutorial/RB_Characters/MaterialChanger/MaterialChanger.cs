using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoundBearGames_ObstacleCourse
{
    public class MaterialChanger : MonoBehaviour
    {
        public Material material;
        public List<GameObject> CurrentObjects = new List<GameObject>();
        public List<Material> CurrentMaterials = new List<Material>();
        public List<Material> NewMaterials = new List<Material>();

        public void ChangeMaterial()
        {
            if (material == null)
            {
                Debug.LogError("No material specified");
            }

            Renderer[] arrMaterials = this.gameObject.GetComponentsInChildren<Renderer>();

            foreach (Renderer r in arrMaterials)
            {
                if (r.gameObject != this.gameObject)
                {
                    Debug.Log("Changing material on: " + r.gameObject.name + " / " + material.name);
                    r.material = material;
                }
            }
        }

        public void ChangeComplexMaterial()
        {
            Debug.Log("Changing multiple materials");

            Dictionary<GameObject, int> ChangeSchedule = new Dictionary<GameObject, int>();

            Renderer[] arrRenderers = this.gameObject.GetComponentsInChildren<Renderer>();

            foreach (Renderer r in arrRenderers)
            {
                if (r.gameObject != this.gameObject)
                {
                    for (int i = 0; i < CurrentObjects.Count; i++)
                    {
                        if (NewMaterials[i] == null)
                        {
                            Debug.LogError("New Material is empty: " + "index " + i);
                            continue;
                        }
                        else if (r.sharedMaterial == CurrentObjects[i].GetComponent<Renderer>().sharedMaterial)
                        {
                            Debug.Log("Change schedule: " + r.gameObject.name + " / " + NewMaterials[i].name);
                            ChangeSchedule.Add(r.gameObject, i);
                            break;
                        }
                    }
                }
            }

            foreach(KeyValuePair<GameObject,int>data in ChangeSchedule)
            {
                Debug.Log("Changing material: " + data.Key.name + " / " + NewMaterials[data.Value].name);
                data.Key.GetComponent<Renderer>().sharedMaterial = NewMaterials[data.Value];
            }
        }

        public void IdentifyMaterials()
        {
            Renderer[] arrRenderers = this.gameObject.GetComponentsInChildren<Renderer>();

            foreach (Renderer r in arrRenderers)
            {
                bool skip = false;

                if (r.gameObject != this.gameObject)
                {
                    foreach(GameObject obj in CurrentObjects)
                    {
                        if (obj.GetComponent<Renderer>().sharedMaterial == r.sharedMaterial)
                        {
                            skip = true;
                            break;
                        }
                    }

                    if (!skip)
                    {
                        CurrentObjects.Add(r.gameObject);
                        CurrentMaterials.Add(r.sharedMaterial);
                        NewMaterials.Add(null);
                    }
                }
            }
        }
    }
}