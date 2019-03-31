using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial_1
{
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
		private static T _instance;

		public static T Instance {
			get {
				if (_instance == null) {
					//_instance = (T)FindObjectOfType (typeof (T));
					if (_instance == null) {
                        GameObject obj = new GameObject ();
						_instance = obj.AddComponent<T> ();

                        obj.name = typeof (T).ToString ();
                        obj.transform.position = Vector3.zero;
                        obj.transform.rotation = Quaternion.identity;
                    }
				}
				return _instance;
			}
		}
	}
}