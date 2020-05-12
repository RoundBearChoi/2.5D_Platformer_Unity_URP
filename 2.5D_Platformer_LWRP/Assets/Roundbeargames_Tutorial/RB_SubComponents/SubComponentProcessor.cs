using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class SubComponentProcessor : MonoBehaviour
    {
        public Dictionary<SubComponents, SubComponent> ComponentsDic = new Dictionary<SubComponents, SubComponent>();
        public CharacterControl control;

        [Space(15)] public BlockingObjData blockingData;
        [Space(15)] public LedgeGrabData ledgeGrabData;

        private void Awake()
        {
            control = GetComponentInParent<CharacterControl>();
        }

        public void FixedUpdateSubComponents()
        {
            FixedUpdateSubComponent(SubComponents.LEDGECHECKER);
            FixedUpdateSubComponent(SubComponents.RAGDOLL);
            FixedUpdateSubComponent(SubComponents.BLOCKINGOBJECTS);
        }

        public void UpdateSubComponents()
        {
            UpdateSubComponent(SubComponents.MANUALINPUT);
        }

        void UpdateSubComponent(SubComponents type)
        {
            if (ComponentsDic.ContainsKey(type))
            {
                ComponentsDic[type].OnUpdate();
            }
        }

        void FixedUpdateSubComponent(SubComponents type)
        {
            if (ComponentsDic.ContainsKey(type))
            {
                ComponentsDic[type].OnFixedUpdate();
            }
        }
    }
}
