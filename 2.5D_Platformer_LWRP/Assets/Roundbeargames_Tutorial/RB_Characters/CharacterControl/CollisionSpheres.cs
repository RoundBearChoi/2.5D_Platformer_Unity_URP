using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CollisionSpheres : SubComponent
    {
        public CollisionData collisionData;

        private void Start()
        {
            collisionData = new CollisionData
            {
                BottomSpheres = new List<GameObject>(),
                FrontSpheres = new List<GameObject>(),
                BackSpheres = new List<GameObject>(),
                UpSpheres = new List<GameObject>(),

                FrontOverlapCheckers = new List<OverlapChecker>(),
                AllOverlapCheckers = new List<OverlapChecker>(),

                Reposition_FrontSpheres = Reposition_FrontSpheres,
                Reposition_BottomSpheres = Reposition_BottomSpheres,
                Reposition_BackSpheres = Reposition_BackSpheres,
                Reposition_UpSpheres = Reposition_UpSpheres,
            };

            subComponentProcessor.collisionData = collisionData;
            subComponentProcessor.ComponentsDic.Add(SubComponentType.COLLISION_SPHERES, this);

            SetColliderSpheres();
        }

        public override void OnFixedUpdate()
        {
            foreach (OverlapChecker c in collisionData.AllOverlapCheckers)
            {
                c.UpdateChecker();
            }
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        void SetColliderSpheres()
        {
            // bottom

            for (int i = 0; i < 5; i++)
            {
                GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)),
                    Vector3.zero, Quaternion.identity) as GameObject;
                collisionData.BottomSpheres.Add(obj);
                obj.transform.parent = this.transform.Find("Bottom");
            }

            Reposition_BottomSpheres();

            // top

            for (int i = 0; i < 5; i++)
            {
                GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)),
                    Vector3.zero, Quaternion.identity) as GameObject;
                collisionData.UpSpheres.Add(obj);
                obj.transform.parent = this.transform.Find("Up");
            }

            Reposition_UpSpheres();

            // front

            for (int i = 0; i < 10; i++)
            {
                GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)),
                    Vector3.zero, Quaternion.identity) as GameObject;

                collisionData.FrontSpheres.Add(obj);
                collisionData.FrontOverlapCheckers.Add(obj.GetComponent<OverlapChecker>());

                obj.transform.parent = this.transform.Find("Front");
            }

            Reposition_FrontSpheres();

            // back

            for (int i = 0; i < 10; i++)
            {
                GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)),
                    Vector3.zero, Quaternion.identity) as GameObject;
                collisionData.BackSpheres.Add(obj);
                obj.transform.parent = this.transform.Find("Back");
            }

            Reposition_BackSpheres();

            // add everything

            OverlapChecker[] arr = this.gameObject.GetComponentsInChildren<OverlapChecker>();
            collisionData.AllOverlapCheckers.Clear();
            collisionData.AllOverlapCheckers.AddRange(arr);
        }

        void Reposition_FrontSpheres()
        {
            float bottom = control.boxCollider.bounds.center.y - (control.boxCollider.bounds.size.y / 2f);
            float top = control.boxCollider.bounds.center.y + (control.boxCollider.bounds.size.y / 2f);
            float front = control.boxCollider.bounds.center.z + (control.boxCollider.bounds.size.z / 2f);

            collisionData.FrontSpheres[0].transform.localPosition =
                new Vector3(0f, bottom + 0.05f, front) - control.transform.position;

            collisionData.FrontSpheres[1].transform.localPosition =
                new Vector3(0f, top, front) - control.transform.position;

            float interval = (top - bottom + 0.05f) / 9;

            for (int i = 2; i < collisionData.FrontSpheres.Count; i++)
            {
                collisionData.FrontSpheres[i].transform.localPosition =
                    new Vector3(0f, bottom + (interval * (i - 1)), front) - control.transform.position;
            }
        }

        void Reposition_BackSpheres()
        {
            float bottom = control.boxCollider.bounds.center.y - (control.boxCollider.bounds.size.y / 2f);
            float top = control.boxCollider.bounds.center.y + (control.boxCollider.bounds.size.y / 2f);
            float back = control.boxCollider.bounds.center.z - (control.boxCollider.bounds.size.z / 2f);

            collisionData.BackSpheres[0].transform.localPosition =
                new Vector3(0f, bottom + 0.05f, back) - control.transform.position;

            collisionData.BackSpheres[1].transform.localPosition =
                new Vector3(0f, top, back) - control.transform.position;

            float interval = (top - bottom + 0.05f) / 9;

            for (int i = 2; i < collisionData.BackSpheres.Count; i++)
            {
                collisionData.BackSpheres[i].transform.localPosition =
                    new Vector3(0f, bottom + (interval * (i - 1)), back) - control.transform.position;
            }
        }

        void Reposition_BottomSpheres()
        {
            float bottom = control.boxCollider.bounds.center.y - (control.boxCollider.bounds.size.y / 2f);
            float front = control.boxCollider.bounds.center.z + (control.boxCollider.bounds.size.z / 2f);
            float back = control.boxCollider.bounds.center.z - (control.boxCollider.bounds.size.z / 2f);

            collisionData.BottomSpheres[0].transform.localPosition =
                new Vector3(0f, bottom, back) - control.transform.position;

            collisionData.BottomSpheres[1].transform.localPosition =
                new Vector3(0f, bottom, front) - control.transform.position;

            float interval = (front - back) / 4;

            for (int i = 2; i < collisionData.BottomSpheres.Count; i++)
            {
                collisionData.BottomSpheres[i].transform.localPosition =
                    new Vector3(0f, bottom, back + (interval * (i - 1))) - control.transform.position;
            }
        }

        void Reposition_UpSpheres()
        {
            float top = control.boxCollider.bounds.center.y + (control.boxCollider.bounds.size.y / 2f);
            float front = control.boxCollider.bounds.center.z + (control.boxCollider.bounds.size.z / 2f);
            float back = control.boxCollider.bounds.center.z - (control.boxCollider.bounds.size.z / 2f);

            collisionData.UpSpheres[0].transform.localPosition =
                new Vector3(0f, top, back) - control.transform.position;

            collisionData.UpSpheres[1].transform.localPosition =
                new Vector3(0f, top, front) - control.transform.position;

            float interval = (front - back) / 4;

            for (int i = 2; i < collisionData.UpSpheres.Count; i++)
            {
                collisionData.UpSpheres[i].transform.localPosition =
                    new Vector3(0f, top, back + (interval * (i - 1))) - control.transform.position;
            }
        }
    }
}