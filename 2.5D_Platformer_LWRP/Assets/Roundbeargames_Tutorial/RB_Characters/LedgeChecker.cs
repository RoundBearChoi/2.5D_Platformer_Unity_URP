using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class LedgeChecker : MonoBehaviour
    {
        public bool IsGrabbingLedge;
        Ledge ledge = null;

        private void OnTriggerEnter(Collider other)
        {
            ledge = other.gameObject.GetComponent<Ledge>();
            if (ledge != null)
            {
                IsGrabbingLedge = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            ledge = other.gameObject.GetComponent<Ledge>();
            if (ledge != null)
            {
                IsGrabbingLedge = false;
            }
        }
    }
}