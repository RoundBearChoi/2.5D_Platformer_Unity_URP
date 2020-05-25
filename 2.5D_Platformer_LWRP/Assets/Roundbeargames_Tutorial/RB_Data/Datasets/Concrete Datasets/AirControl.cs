using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames.Datasets
{
    public enum AirControlBool
    {
        NONE,

        CAN_WALL_JUMP,
        CHECK_WALL_BLOCK
    }

    public class AirControl : Dataset
    {
        private void Start()
        {
            BoolDictionary.Add((int)AirControlBool.CAN_WALL_JUMP, false);
            BoolDictionary.Add((int)AirControlBool.CHECK_WALL_BLOCK, false);
        }
    }
}
