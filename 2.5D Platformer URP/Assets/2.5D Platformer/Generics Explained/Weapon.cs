using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon<T> : MonoBehaviour where T: MonoBehaviour
{
    void Start()
    {
        Declare();
    }

    void Declare()
    {
        Debug.Log("hello i am a " + typeof(T));

        if (!this.gameObject.name.Contains("copy"))
        {
            GameObject copy = new GameObject();
            copy.name = "copy of a " + typeof(T);

            copy.AddComponent<T>();
        }
    }
}
