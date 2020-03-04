using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class AttackInfo : MonoBehaviour
    {
        public CharacterControl Attacker = null;
        public Attack AttackAbility;
        public List<AttackPartType> AttackParts = new List<AttackPartType>();
        public DeathType deathType;
        public bool MustCollide;
        public bool MustFaceAttacker;
        public float LethalRange;
        public int MaxHits;
        public int CurrentHits;
        public bool isRegisterd;
        public bool isFinished;
        public bool UseRagdollDeath;
        public List<CharacterControl> RegisteredTargets = new List<CharacterControl>();

        public void ResetInfo(Attack attack, CharacterControl attacker)
        {
            isRegisterd = false;
            isFinished = false;
            AttackAbility = attack;
            Attacker = attacker;
            RegisteredTargets.Clear();
        }

        public void Register(Attack attack)
        {
            isRegisterd = true;

            AttackAbility = attack;
            AttackParts = attack.AttackParts;
            MustCollide = attack.MustCollide;
            MustFaceAttacker = attack.MustFaceAttacker;
            LethalRange = attack.LethalRange;
            MaxHits = attack.MaxHits;
            CurrentHits = 0;
        }

        public void CopyInfo(Attack attack, CharacterControl attacker)
        {
            Attacker = attacker;
            AttackAbility = attack;
            AttackParts = attack.AttackParts;
            MustCollide = attack.MustCollide;
            MustFaceAttacker = attack.MustFaceAttacker;
            LethalRange = attack.LethalRange;
            MaxHits = attack.MaxHits;
            CurrentHits = 0;
        }

        private void OnDisable()
        {
            isFinished = true;
        }
    }
}