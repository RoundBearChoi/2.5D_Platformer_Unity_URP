using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class AttackManager : Singleton<AttackManager>
    {
        public GameObject ActiveAttacks;
        public List<AttackCondition> CurrentAttacks = new List<AttackCondition>();

        public void ForceDeregister(CharacterControl control)
        {
            foreach(AttackCondition info in CurrentAttacks)
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