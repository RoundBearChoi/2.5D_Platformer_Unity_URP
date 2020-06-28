using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CollisionSpheres : SubComponent
    {
        public CollisionSphereData collisionSphereData;

        private void Start()
        {
            collisionSphereData = new CollisionSphereData
            {
                BottomSpheres = new GameObject[5], //new List<GameObject>(),
                FrontSpheres = new GameObject[10], //List<GameObject>(),
                BackSpheres = new GameObject[10], //new List<GameObject>(),
                UpSpheres = new GameObject[5], //new List<GameObject>(),

                FrontOverlapCheckers = new List<OverlapChecker>(),
                AllOverlapCheckers = new List<OverlapChecker>(),

                Reposition_FrontSpheres = Reposition_FrontSpheres,
                Reposition_BottomSpheres = Reposition_BottomSpheres,
                Reposition_BackSpheres = Reposition_BackSpheres,
                Reposition_UpSpheres = Reposition_UpSpheres,
            };

            subComponentProcessor.collisionSphereData = collisionSphereData;
            subComponentProcessor.ComponentsDic.Add(SubComponentType.COLLISION_SPHERES, this);

            SetColliderSpheres();
        }

        public override void OnFixedUpdate()
        {
            foreach (OverlapChecker c in collisionSphereData.AllOverlapCheckers)
            {
                c.UpdateChecker();
            }
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        GameObject LoadCollisionSphere()
        {
            return Instantiate(Resources.Load("CollisionSphere", typeof(GameObject)),
                    Vector3.zero, Quaternion.identity) as GameObject;
        }

        void SetColliderSpheres()
        {
            // bottom

            for (int i = 0; i < 5; i++)
            {
                GameObject obj = LoadCollisionSphere();

                collisionSphereData.BottomSpheres[i] = obj;
                obj.transform.parent = this.transform.Find("Bottom");
            }

            Reposition_BottomSpheres();

            // top

            for (int i = 0; i < 5; i++)
            {
                GameObject obj = LoadCollisionSphere();

                collisionSphereData.UpSpheres[i] = obj;
                obj.transform.parent = this.transform.Find("Up");
            }

            Reposition_UpSpheres();

            // front

            for (int i = 0; i < 10; i++)
            {
                GameObject obj = LoadCollisionSphere();

                collisionSphereData.FrontSpheres[i] = obj;
                collisionSphereData.FrontOverlapCheckers.Add(obj.GetComponent<OverlapChecker>());

                obj.transform.parent = this.transform.Find("Front");
            }

            Reposition_FrontSpheres();

            // back

            for (int i = 0; i < 10; i++)
            {
                GameObject obj = LoadCollisionSphere();

                collisionSphereData.BackSpheres[i] = obj;
                obj.transform.parent = this.transform.Find("Back");
            }

            Reposition_BackSpheres();

            // add everything

            OverlapChecker[] arr = this.gameObject.GetComponentsInChildren<OverlapChecker>();
            collisionSphereData.AllOverlapCheckers.Clear();
            collisionSphereData.AllOverlapCheckers.AddRange(arr);
        }

        void Reposition_FrontSpheres()
        {
            float bottom = control.boxCollider.bounds.center.y - (control.boxCollider.bounds.size.y / 2f);
            float top = control.boxCollider.bounds.center.y + (control.boxCollider.bounds.size.y / 2f);
            float front = control.boxCollider.bounds.center.z + (control.boxCollider.bounds.size.z / 2f);

            collisionSphereData.FrontSpheres[0].transform.localPosition =
                new Vector3(0f, bottom + 0.05f, front) - control.transform.position;

            collisionSphereData.FrontSpheres[1].transform.localPosition =
                new Vector3(0f, top, front) - control.transform.position;

            float interval = (top - bottom + 0.05f) / 9;

            for (int i = 2; i < collisionSphereData.FrontSpheres.Length; i++)
            {
                collisionSphereData.FrontSpheres[i].transform.localPosition =
                    new Vector3(0f, bottom + (interval * (i - 1)), front) - control.transform.position;
            }
        }

        void Reposition_BackSpheres()
        {
            float bottom = control.boxCollider.bounds.center.y - (control.boxCollider.bounds.size.y / 2f);
            float top = control.boxCollider.bounds.center.y + (control.boxCollider.bounds.size.y / 2f);
            float back = control.boxCollider.bounds.center.z - (control.boxCollider.bounds.size.z / 2f);

            collisionSphereData.BackSpheres[0].transform.localPosition =
                new Vector3(0f, bottom + 0.05f, back) - control.transform.position;

            collisionSphereData.BackSpheres[1].transform.localPosition =
                new Vector3(0f, top, back) - control.transform.position;

            float interval = (top - bottom + 0.05f) / 9;

            for (int i = 2; i < collisionSphereData.BackSpheres.Length; i++)
            {
                collisionSphereData.BackSpheres[i].transform.localPosition =
                    new Vector3(0f, bottom + (interval * (i - 1)), back) - control.transform.position;
            }
        }

        void Reposition_BottomSpheres()
        {
            float bottom = control.boxCollider.bounds.center.y - (control.boxCollider.bounds.size.y / 2f);
            float front = control.boxCollider.bounds.center.z + (control.boxCollider.bounds.size.z / 2f);
            float back = control.boxCollider.bounds.center.z - (control.boxCollider.bounds.size.z / 2f);

            collisionSphereData.BottomSpheres[0].transform.localPosition =
                new Vector3(0f, bottom, back) - control.transform.position;

            collisionSphereData.BottomSpheres[1].transform.localPosition =
                new Vector3(0f, bottom, front) - control.transform.position;

            float interval = (front - back) / 4;

            for (int i = 2; i < collisionSphereData.BottomSpheres.Length; i++)
            {
                collisionSphereData.BottomSpheres[i].transform.localPosition =
                    new Vector3(0f, bottom, back + (interval * (i - 1))) - control.transform.position;
            }
        }

        void Reposition_UpSpheres()
        {
            float top = control.boxCollider.bounds.center.y + (control.boxCollider.bounds.size.y / 2f);
            float front = control.boxCollider.bounds.center.z + (control.boxCollider.bounds.size.z / 2f);
            float back = control.boxCollider.bounds.center.z - (control.boxCollider.bounds.size.z / 2f);

            collisionSphereData.UpSpheres[0].transform.localPosition =
                new Vector3(0f, top, back) - control.transform.position;

            collisionSphereData.UpSpheres[1].transform.localPosition =
                new Vector3(0f, top, front) - control.transform.position;

            float interval = (front - back) / 4;

            for (int i = 2; i < collisionSphereData.UpSpheres.Length; i++)
            {
                collisionSphereData.UpSpheres[i].transform.localPosition =
                    new Vector3(0f, top, back + (interval * (i - 1))) - control.transform.position;
            }
        }
    }
}