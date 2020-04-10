using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum SubComponents
    {
        NONE,
        MANUALINPUT,
    }

    public enum BoolData
    {
        NONE,
        DOUBLETAP_UP,
        DOUBLETAP_DOWN,
    }

    public abstract class SubComponent : MonoBehaviour
    {
        public CharacterControl control;

        private void Awake()
        {
            control = this.gameObject.GetComponentInParent<CharacterControl>();
        }

        public abstract void OnUpdate();
    }
}