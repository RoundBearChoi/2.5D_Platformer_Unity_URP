using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class JumpData
    {
        //public bool Jumped;
        public Dictionary<int, bool> DicJumped;
        public bool CanWallJump;
        public bool CheckWallBlock;
    }
}