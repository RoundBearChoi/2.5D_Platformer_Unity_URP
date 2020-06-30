using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class DamageData
    {
        public AttackCondition BlockedAttack;
        public float hp;
        public Attack MarioStompAttack;
        public Attack AxeThrow;

        public NormalDamageTaken normalDamageTaken;
        public CollateralDamageTaken collateralDamageTaken;

        public delegate bool ReturnBool();
        public delegate void DoSomething(AttackCondition info);

        public ReturnBool IsDead;
        public DoSomething TakeDamage;

        [System.Serializable]
        public class NormalDamageTaken
        {
            public NormalDamageTaken(CharacterControl attacker,
                Attack attack,
                TriggerDetector damagee,
                GameObject damager)
            {
                Attacker = attacker;
                Attack = attack;
                Damagee = damagee;
                Damager = damager;
            }

            public CharacterControl Attacker = null;
            public Attack Attack = null;
            public GameObject Damager = null;
            public TriggerDetector Damagee = null;
        }

        [System.Serializable]
        public class CollateralDamageTaken
        {
            public Vector3 Velocity = Vector3.zero;
            public TriggerDetector Damagee = null;
        }
    }
}