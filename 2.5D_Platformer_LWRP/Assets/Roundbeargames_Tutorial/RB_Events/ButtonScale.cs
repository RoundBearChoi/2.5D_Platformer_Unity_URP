using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum UIParameters
    {
        ScaleUp,
    }

    public class ButtonScale : MonoBehaviour
    {
        GameEventListener listener;

        private void Awake()
        {
            listener = GetComponent<GameEventListener>();
        }

        public void ScaleUpButton()
        {
            listener.gameEvent.EventObj.GetComponent<Animator>().SetBool(UIParameters.ScaleUp.ToString(), true);
        }

        public void ResetButtonScale()
        {
            listener.gameEvent.EventObj.GetComponent<Animator>().SetBool(UIParameters.ScaleUp.ToString(), false);
        }
    }
}