using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames.Datasets
{
    public abstract class Dataset : MonoBehaviour
    {
        protected Dictionary<int, bool> BoolDictionary = new Dictionary<int, bool>();
        protected Dictionary<int, float> FloatDictionary = new Dictionary<int, float>();
        protected Dictionary<int, Vector3> Vector3Dictionary = new Dictionary<int, Vector3>();

        public bool GetBool(int index)
        {
            return BoolDictionary[index];
        }

        public void SetBool(int index, bool b)
        {
            BoolDictionary[index] = b;
        }

        public float GetFloat(int index)
        {
            return FloatDictionary[index];
        }

        public void SetFloat(int index, float f)
        {
            FloatDictionary[index] = f;
        }

        public Vector3 GetVector3(int index)
        {
            return Vector3Dictionary[index];
        }

        public void SetVector3(int index, Vector3 v)
        {
            Vector3Dictionary[index] = v;
        }
    }
}