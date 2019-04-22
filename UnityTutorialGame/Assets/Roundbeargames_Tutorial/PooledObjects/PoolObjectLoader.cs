using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public enum PoolObjectType
    {
        ATTACKINFO,
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
            }

            return obj.GetComponent<PoolObject>();
        }
    }
}