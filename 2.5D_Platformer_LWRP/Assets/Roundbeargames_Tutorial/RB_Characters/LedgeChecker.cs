using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class LedgeChecker : MonoBehaviour
    {
        public bool IsGrabbingLedge;
        public Ledge GrabbedLedge;
        public Vector3 LedgeCalibration = new Vector3();
        Ledge CheckLedge = null;

        private void OnTriggerEnter(Collider other)
        {
            CheckLedge = other.gameObject.GetComponent<Ledge>();
            if (CheckLedge != null)
            {
                IsGrabbingLedge = true;
                GrabbedLedge = CheckLedge;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            CheckLedge = other.gameObject.GetComponent<Ledge>();
            if (CheckLedge != null)
            {
                IsGrabbingLedge = false;
                //GrabbedLedge = null;
            }
        }
    }
}