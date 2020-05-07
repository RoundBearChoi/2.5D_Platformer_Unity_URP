using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames.Datasets
{
    public enum AirControlBool
    {
        NONE,

        JUMPED,
        CANCEL_PULL,
        CAN_WALL_JUMP,
        CHECK_WALL_BLOCK
    }

    public enum AirControlFloat
    {
        NONE,

        AIR_MOMENTUM,
    }

    public enum AirControlVector3
    {
        NONE,

        MAX_FALL_VELOCITY,
    }

    public class AirControl : Dataset
    {
        private void Start()
        {
            BoolDictionary.Add((int)AirControlBool.JUMPED, false);
            BoolDictionary.Add((int)AirControlBool.CANCEL_PULL, false);
            BoolDictionary.Add((int)AirControlBool.CAN_WALL_JUMP, false);
            BoolDictionary.Add((int)AirControlBool.CHECK_WALL_BLOCK, false);

            FloatDictionary.Add((int)AirControlFloat.AIR_MOMENTUM, 0f);

            Vector3Dictionary.Add((int)AirControlVector3.MAX_FALL_VELOCITY, Vector3.zero);
        }
    }
}
