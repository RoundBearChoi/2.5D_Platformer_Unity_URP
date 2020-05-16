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
        }

        public override void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}