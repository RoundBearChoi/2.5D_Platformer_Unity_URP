using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent gameEvent;
        [Space(10)]
        public UnityEngine.Events.UnityEvent response;

        private void Start()
        {
            if (gameEvent != null)
            {
                if (!gameEvent.ListListeners.Contains(this))
                {
                    gameEvent.ListListeners.Add(this);
                }
            }
        }

        public void OnRaiseEvent()
        {
            response.Invoke();
        }
    }
}