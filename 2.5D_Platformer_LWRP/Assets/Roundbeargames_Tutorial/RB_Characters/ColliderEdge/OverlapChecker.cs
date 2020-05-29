using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class OverlapChecker : MonoBehaviour
    {
        CharacterControl control;
        public Collider[] arrColliders;
        public bool ObjIsOverlapping;

        private void Start()
        {
            control = this.transform.root.gameObject.GetComponent<CharacterControl>();
        }

        public void UpdateChecker()
        {
            try
            {
                if (control.JUMP_DATA.CheckWallBlock)
                {
                    if (control.collisionSpheres.FrontOverlapCheckers.Contains(this))
                    {
                        ObjIsOverlapping = CheckObj();
                    }
                }
                else
                {
                    ObjIsOverlapping = false;
                }
            }
            catch(System.Exception e)
            {
                Debug.Log(e);
            }
        }

        private bool CheckObj()
        {
            arrColliders = Physics.OverlapSphere(this.transform.position, 0.13f);

            foreach (Collider c in arrColliders)
            {
                if (CharacterManager.Instance.GetCharacter(c.transform.root.gameObject) == null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}