using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class BoxColliderUpdater : SubComponent
    {
        public BoxColliderData boxColliderData;

        private void Start()
        {
            boxColliderData = new BoxColliderData
            {
                IsUpdatingSpheres = false,
                IsLanding = false,

                Size_Update_Speed = 0f,
                Center_Update_Speed = 0f,

                TargetSize = Vector3.zero,
                TargetCenter = Vector3.zero,
                LandingPosition = Vector3.zero,
            };

            subComponentProcessor.boxColliderData = boxColliderData;
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.BOX_COLLIDER_UPDATER] = this;
            //subComponentProcessor.ComponentsDic.Add(SubComponentType.BOX_COLLIDER_UPDATER, this);
        }

        public override void OnFixedUpdate()
        {
            boxColliderData.IsUpdatingSpheres = false;

            UpdateBoxCollider_Size();
            UpdateBoxCollider_Center();

            if (boxColliderData.IsUpdatingSpheres)
            {
                control.COLLISION_SPHERE_DATA.Reposition_FrontSpheres();
                control.COLLISION_SPHERE_DATA.Reposition_BottomSpheres();
                control.COLLISION_SPHERE_DATA.Reposition_BackSpheres();
                control.COLLISION_SPHERE_DATA.Reposition_UpSpheres();

                if (boxColliderData.IsLanding)
                {
                    //Debug.Log("repositioning y");
                    control.RIGID_BODY.MovePosition(new Vector3(
                        0f,
                        boxColliderData.LandingPosition.y,
                        this.transform.position.z));
                }
            }
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateBoxCollider_Size()
        {
            if (!control.ANIMATION_DATA.IsRunning(typeof(UpdateBoxCollider)))
            {
                return;
            }

            if (Vector3.SqrMagnitude(control.boxCollider.size - boxColliderData.TargetSize) > 0.00001f)
            {
                control.boxCollider.size = Vector3.Lerp(control.boxCollider.size,
                    boxColliderData.TargetSize,
                    Time.deltaTime * boxColliderData.Size_Update_Speed);

                boxColliderData.IsUpdatingSpheres = true;
            }
        }

        public void UpdateBoxCollider_Center()
        {
            if (!control.ANIMATION_DATA.IsRunning(typeof(UpdateBoxCollider)))
            {
                return;
            }

            if (Vector3.SqrMagnitude(control.boxCollider.center - boxColliderData.TargetCenter) > 0.00001f)
            {
                control.boxCollider.center = Vector3.Lerp(control.boxCollider.center,
                    boxColliderData.TargetCenter,
                    Time.deltaTime * boxColliderData.Center_Update_Speed);

                boxColliderData.IsUpdatingSpheres = true;
            }
        }
    }
}