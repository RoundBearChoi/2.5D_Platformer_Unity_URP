using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class MomentumCalculator : SubComponent
    {
        public MomentumData momentumData;

        private void Start()
        {
            momentumData = new MomentumData
            {
                Momentum = 0f,
                CalculateMomentum = CalculateMomentum,
            };

            subComponentProcessor.momentumData = momentumData;
        }

        public override void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        void CalculateMomentum(float speed, float maxMomentum)
        {
            if (!control.BLOCKING_DATA.RightSideBlocked())
            {
                if (control.MoveRight)
                {
                    momentumData.Momentum += speed;
                }
            }

            if (!control.BLOCKING_DATA.LeftSideBlocked())
            {
                if (control.MoveLeft)
                {
                    momentumData.Momentum -= speed;
                }
            }

            if (control.BLOCKING_DATA.RightSideBlocked() || control.BLOCKING_DATA.LeftSideBlocked())
            {
                float lerped = Mathf.Lerp(momentumData.Momentum, 0f, Time.deltaTime * 1.5f);
                momentumData.Momentum = lerped;
            }


            if (Mathf.Abs(momentumData.Momentum) >= maxMomentum)
            {
                if (momentumData.Momentum > 0f)
                {
                    momentumData.Momentum = maxMomentum;
                }
                else if (momentumData.Momentum < 0f)
                {
                    momentumData.Momentum = -maxMomentum;
                }
            }
        }
    }
}