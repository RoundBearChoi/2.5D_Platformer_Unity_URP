using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class DamageData
    {
        public CharacterControl Attacker;
        public Attack Attack;
        public TriggerDetector DamagedTrigger;
        public GameObject AttackingPart;
        public AttackInfo BlockedAttack;

        public delegate bool ReturnBool();

        public ReturnBool IsDead;

        public void SetData(
            CharacterControl attacker,
            Attack attack,
            TriggerDetector damagedTrigger,
            GameObject attackingPart)
        {
            Attacker = attacker;
            Attack = attack;
            DamagedTrigger = damagedTrigger;
            AttackingPart = attackingPart;
        }
    }
}