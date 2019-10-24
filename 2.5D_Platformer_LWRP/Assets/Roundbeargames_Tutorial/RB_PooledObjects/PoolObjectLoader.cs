using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum PoolObjectType
    {
        ATTACKINFO,
        HAMMER_OBJ,
        HAMMER_VFX,
        DAMAGE_WHITE_VFX,
    }

    public class PoolObjectLoader : MonoBehaviour
    {
        public static PoolObject InstantiatePrefab(PoolObjectType objType)
        {
            GameObject obj = null;

            switch (objType)
            {
                case PoolObjectType.ATTACKINFO:
                    {
                        obj = Instantiate(Resources.Load("AttackInfo", typeof(GameObject)) as GameObject);
                        break;
                    }
                case PoolObjectType.HAMMER_OBJ:
                    {
                        obj = Instantiate(Resources.Load("ThorHammer", typeof(GameObject)) as GameObject);
                        break;
                    }
                case PoolObjectType.HAMMER_VFX:
                    {
                        obj = Instantiate(Resources.Load("VFX_HammerDown", typeof(GameObject)) as GameObject);
                        break;
                    }
                case PoolObjectType.DAMAGE_WHITE_VFX:
                    {
                        obj = Instantiate(Resources.Load("VFX_Damage_White", typeof(GameObject)) as GameObject);
                        break;
                    }
            }

            return obj.GetComponent<PoolObject>();
        }
    }
}