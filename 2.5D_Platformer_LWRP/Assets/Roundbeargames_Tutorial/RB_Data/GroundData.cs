using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class GroundData
    {
        public GameObject Ground;
        public ContactPoint[] BoxColliderContacts;
    }
}