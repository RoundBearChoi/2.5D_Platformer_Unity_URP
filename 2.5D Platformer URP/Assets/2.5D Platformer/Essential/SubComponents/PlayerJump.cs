using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class PlayerJump : SubComponent
    {
        public JumpData jumpData;

        private void Start()
        {
            jumpData = new JumpData
            {
                DicJumped = new Dictionary<int, bool>(),
                CanWallJump = false,
                CheckWallBlock = false,
            };

            subComponentProcessor.jumpData = jumpData;
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