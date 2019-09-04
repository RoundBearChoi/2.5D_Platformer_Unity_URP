using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class GameEvent : MonoBehaviour
    {
        List<GameEventListener> ListListeners = new List<GameEventListener>();
        GameObject EventObj;

        public GameObject EVENTOBJ
        {
            get { return EventObj; }
        }

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

        public void Raise(GameObject eventObj)
        {
            EventObj = eventObj;

            foreach (GameEventListener listener in ListListeners)
            {
                listener.OnRaiseEvent();
            }
        }

        public void AddListener(GameEventListener listener)
        {
            if (!ListListeners.Contains(listener))
            {
                ListListeners.Add(listener);
            }
        }
    }
}