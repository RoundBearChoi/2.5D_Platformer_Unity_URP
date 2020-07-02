using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IteratingPlayer : MonoBehaviour
{
    public List<int> IntList = new List<int>();
    public List<string> StringList = new List<string>();

    public void IterateThrough<T>(List<T> targetList)
    {
        foreach(T data in targetList)
        {
            Debug.Log(data);
        }
    }

    private void Start()
    {
        IterateThrough(IntList);
        IterateThrough(StringList);
    }
}
