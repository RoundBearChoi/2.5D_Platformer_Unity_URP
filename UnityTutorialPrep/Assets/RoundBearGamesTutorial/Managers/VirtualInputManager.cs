using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoundBearGames_ObstacleCourse {
	public class VirtualInputManager : MonoBehaviour {

		public static VirtualInputManager Instance = null;

		void Awake () {
			if (Instance == null) {
				Instance = this;
			} else if (Instance != this) {
				Destroy (this.gameObject);
			}
		}

		public bool MoveRight;
		public bool MoveLeft;
	}
}