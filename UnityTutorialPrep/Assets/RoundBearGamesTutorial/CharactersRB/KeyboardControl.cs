using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial_1
{
    public class KeyboardControl : MonoBehaviour
    {
        private CharacterControl characterControl;

        private void Awake()
        {
            characterControl = this.gameObject.GetComponent<CharacterControl>();
        }

        void Update()
        {
            if (ControllerManager.Instance.MoveRight)
            {
                characterControl.MoveRight = true;
            }
            else
            {
                characterControl.MoveRight = false;
            }

            if (ControllerManager.Instance.MoveLeft)
            {
                characterControl.MoveLeft = true;
            }
            else
            {
                characterControl.MoveLeft = false;
            }
        }
    }
}