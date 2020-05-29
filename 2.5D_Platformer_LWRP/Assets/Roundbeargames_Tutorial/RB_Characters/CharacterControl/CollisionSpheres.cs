using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CollisionSpheres : SubComponent
    {
        public List<GameObject> BottomSpheres = new List<GameObject>();
        public List<GameObject> FrontSpheres = new List<GameObject>();
        public List<GameObject> BackSpheres = new List<GameObject>();
        public List<GameObject> UpSpheres = new List<GameObject>();

        public List<OverlapChecker> FrontOverlapCheckers = new List<OverlapChecker>();
        public List<OverlapChecker> AllOverlapCheckers = new List<OverlapChecker>();

        private void Start()
        {
            subComponentProcessor.ComponentsDic.Add(SubComponentType.COLLISION_SPHERES, this);
        }

        public override void OnFixedUpdate()
        {
            foreach (OverlapChecker c in AllOverlapCheckers)
            {
                c.UpdateChecker();
            }
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        public void SetColliderSpheres()
        {
            // bottom

            for (int i = 0; i < 5; i++)
            {
                GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject))
                    , Vector3.zero, Quaternion.identity) as GameObject;
                BottomSpheres.Add(obj);
                obj.transform.parent = this.transform.Find("Bottom");
            }

            Reposition_BottomSpheres();

            // top

            for (int i = 0; i < 5; i++)
            {
                GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject))
                    , Vector3.zero, Quaternion.identity) as GameObject;
                UpSpheres.Add(obj);
                obj.transform.parent = this.transform.Find("Up");
            }

            Reposition_UpSpheres();

            // front

            for (int i = 0; i < 10; i++)
            {
                GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject))
                    , Vector3.zero, Quaternion.identity) as GameObject;

                FrontSpheres.Add(obj);
                FrontOverlapCheckers.Add(obj.GetComponent<OverlapChecker>());

                obj.transform.parent = this.transform.Find("Front");
            }

            Reposition_FrontSpheres();

            // back

            for (int i = 0; i < 10; i++)
            {
                GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject))
                    , Vector3.zero, Quaternion.identity) as GameObject;
                BackSpheres.Add(obj);
                obj.transform.parent = this.transform.Find("Back");
            }

            Reposition_BackSpheres();

            // add everything

            OverlapChecker[] arr = this.gameObject.GetComponentsInChildren<OverlapChecker>();
            AllOverlapCheckers.Clear();
            AllOverlapCheckers.AddRange(arr);
        }

        public void Reposition_FrontSpheres()
        {
            float bottom = control.boxCollider.bounds.center.y - (control.boxCollider.bounds.size.y / 2f);
            float top = control.boxCollider.bounds.center.y + (control.boxCollider.bounds.size.y / 2f);
            float front = control.boxCollider.bounds.center.z + (control.boxCollider.bounds.size.z / 2f);

            FrontSpheres[0].transform.localPosition = new Vector3(0f, bottom + 0.05f, front) - control.transform.position;
            FrontSpheres[1].transform.localPosition = new Vector3(0f, top, front) - control.transform.position;

            float interval = (top - bottom + 0.05f) / 9;

            for (int i = 2; i < FrontSpheres.Count; i++)
            {
                FrontSpheres[i].transform.localPosition = new Vector3(0f, bottom + (interval * (i - 1)), front)
                    - control.transform.position;
            }
        }

        public void Reposition_BackSpheres()
        {
            float bottom = control.boxCollider.bounds.center.y - (control.boxCollider.bounds.size.y / 2f);
            float top = control.boxCollider.bounds.center.y + (control.boxCollider.bounds.size.y / 2f);
            float back = control.boxCollider.bounds.center.z - (control.boxCollider.bounds.size.z / 2f);

            BackSpheres[0].transform.localPosition = new Vector3(0f, bottom + 0.05f, back) - control.transform.position;
            BackSpheres[1].transform.localPosition = new Vector3(0f, top, back) - control.transform.position;

            float interval = (top - bottom + 0.05f) / 9;

            for (int i = 2; i < BackSpheres.Count; i++)
            {
                BackSpheres[i].transform.localPosition = new Vector3(0f, bottom + (interval * (i - 1)), back)
                    - control.transform.position;
            }
        }

        public void Reposition_BottomSpheres()
        {
            float bottom = control.boxCollider.bounds.center.y - (control.boxCollider.bounds.size.y / 2f);
            float front = control.boxCollider.bounds.center.z + (control.boxCollider.bounds.size.z / 2f);
            float back = control.boxCollider.bounds.center.z - (control.boxCollider.bounds.size.z / 2f);

            BottomSpheres[0].transform.localPosition = new Vector3(0f, bottom, back) - control.transform.position;
            BottomSpheres[1].transform.localPosition = new Vector3(0f, bottom, front) - control.transform.position;

            float interval = (front - back) / 4;

            for (int i = 2; i < BottomSpheres.Count; i++)
            {
                BottomSpheres[i].transform.localPosition = new Vector3(0f, bottom, back + (interval * (i - 1)))
                    - control.transform.position;
            }
        }

        public void Reposition_UpSpheres()
        {
            float top = control.boxCollider.bounds.center.y + (control.boxCollider.bounds.size.y / 2f);
            float front = control.boxCollider.bounds.center.z + (control.boxCollider.bounds.size.z / 2f);
            float back = control.boxCollider.bounds.center.z - (control.boxCollider.bounds.size.z / 2f);

            UpSpheres[0].transform.localPosition = new Vector3(0f, top, back) - control.transform.position;
            UpSpheres[1].transform.localPosition = new Vector3(0f, top, front) - control.transform.position;

            float interval = (front - back) / 4;

            for (int i = 2; i < UpSpheres.Count; i++)
            {
                UpSpheres[i].transform.localPosition = new Vector3(0f, top, back + (interval * (i - 1)))
                    - control.transform.position;
            }
        }
    }
}