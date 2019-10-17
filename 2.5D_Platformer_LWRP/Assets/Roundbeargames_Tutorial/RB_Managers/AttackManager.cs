using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class AttackManager : Singleton<AttackManager>
    {
        public List<AttackInfo> CurrentAttacks = new List<AttackInfo>();

        public void ForceDeregister(CharacterControl control)
        {
            foreach(AttackInfo info in CurrentAttacks)
            {
                if (info.Attacker == control)
                {
                    info.isFinished = true;
                    info.GetComponent<PoolObject>().TurnOff();
                }
            }
        }
    }
}