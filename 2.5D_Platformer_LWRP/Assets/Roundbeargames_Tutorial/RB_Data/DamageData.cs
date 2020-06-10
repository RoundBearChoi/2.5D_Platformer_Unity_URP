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
        public AttackCondition BlockedAttack;
        public float hp;
        public Attack MarioStompAttack;
        public Attack AxeThrow;

        public delegate bool ReturnBool();
        public delegate void DoSomething(AttackCondition info);

        public ReturnBool IsDead;
        public DoSomething TakeDamage;

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