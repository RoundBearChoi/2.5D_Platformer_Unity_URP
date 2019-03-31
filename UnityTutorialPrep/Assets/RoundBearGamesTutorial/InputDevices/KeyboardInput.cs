using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial_1
{
	public class KeyboardInput : MonoBehaviour {
		void Update () {
			if (Input.GetKey (KeyCode.D)) {
				ControllerManager.Instance.MoveRight = true;
			} else {
                ControllerManager.Instance.MoveRight = false;
			}

			if (Input.GetKey (KeyCode.A)) {
                ControllerManager.Instance.MoveLeft = true;
			} else {
                ControllerManager.Instance.MoveLeft = false;
			}
		}
	}
}