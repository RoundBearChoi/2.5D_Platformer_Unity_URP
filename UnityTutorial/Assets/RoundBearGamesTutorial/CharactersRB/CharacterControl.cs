using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial_1
{
    public enum TransitionParameter
    {
        Move,
    }

    public class CharacterControl : MonoBehaviour
    {
        public Animator animator;
        public Material material;

        public bool MoveRight;
        public bool MoveLeft;

        public void ChangeMaterial()
        {
            if (material == null)
            {
                Debug.LogError("No material specified");
            }

            Renderer[] arrMaterials = this.gameObject.GetComponentsInChildren<Renderer>();

            foreach (Renderer r in arrMaterials)
            {
                if (r.gameObject != this.gameObject)
                {
                    Debug.Log("Changing material on: " + r.gameObject.name);
                    r.material = material;
                }
            }
        }

        public void FaceForward(bool forward)
        {
            if (forward)
            {
                if (this.transform.forward.z < 0)
                {
                    this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
            }
            else
            {
                if (this.transform.forward.z > 0)
                {
                    this.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                }
            }
        }
    }
}