using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames.Datasets
{
    public class DataProcessor : MonoBehaviour
    {
        Dictionary<System.Type, Dataset> DicDatasets = new Dictionary<System.Type, Dataset>();

        public void InitializeSets(System.Type[] arr)
        {
            foreach(System.Type t in arr)
            {
                GameObject newObj = new GameObject();
                newObj.transform.parent = this.transform;
                newObj.transform.localPosition = Vector3.zero;
                newObj.transform.localRotation = Quaternion.identity;
                newObj.name = t.Name;

                Dataset s = (Dataset)newObj.AddComponent(t);

                DicDatasets.Add(t, s);
            }
        }

        public Dataset GetDataset(System.Type type)
        {
            return DicDatasets[type];
        }
    }
}