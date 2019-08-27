using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class GameEvent : MonoBehaviour
    {
        public List<GameEventListener> ListListeners = new List<GameEventListener>();

        private void Awake()
        {
            ListListeners.Clear();
        }

        public void Raise()
        {
            foreach(GameEventListener listener in ListListeners)
            {
                listener.OnRaiseEvent();
            }
        }
    }
}