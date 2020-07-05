using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class VerticalVelocity : SubComponent
    {
        public VerticalVelocityData verticalVelocityData;

        private void Start()
        {
            verticalVelocityData = new VerticalVelocityData
            {
                NoJumpCancel = false,
                MaxWallSlideVelocity = Vector3.zero,
            };

            subComponentProcessor.verticalVelocityData = verticalVelocityData;
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.VERTICAL_VELOCITY] = this;
            //subComponentProcessor.ComponentsDic.Add(SubComponentType.VERTICAL_VELOCITY, this);
        }

        public override void OnFixedUpdate()
        {
            // jump cancel after letting go
            if (!verticalVelocityData.NoJumpCancel)
            {
                if (control.RIGID_BODY.velocity.y > 0f && !control.Jump)
                {
                    control.RIGID_BODY.velocity -= (Vector3.up * control.RIGID_BODY.velocity.y * 0.1f);
                }
            }

            // slow down wallslide
            if (verticalVelocityData.MaxWallSlideVelocity.y != 0f)
            {
                if (control.RIGID_BODY.velocity.y <= verticalVelocityData.MaxWallSlideVelocity.y)
                {
                    control.RIGID_BODY.velocity = verticalVelocityData.MaxWallSlideVelocity;
                }
            }
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}