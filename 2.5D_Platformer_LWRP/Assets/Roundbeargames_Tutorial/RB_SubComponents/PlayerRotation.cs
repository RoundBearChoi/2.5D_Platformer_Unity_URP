using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class PlayerRotation : SubComponent
    {
        public RotationData rotationData;

        private void Start()
        {
            rotationData = new RotationData
            {
                LockEarlyTurn = false,
                LockDirectionNextState = false,
                EarlyTurnIsLocked = EarlyTurnIsLocked,
            };

            subComponentProcessor.rotationData = rotationData;
        }

        public override void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        bool EarlyTurnIsLocked()
        {
            if (rotationData.LockEarlyTurn || rotationData.LockDirectionNextState)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}