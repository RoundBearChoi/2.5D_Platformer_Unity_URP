using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/Attack")]
    public class Attack : StateData
    {
        public bool debug;
        public float StartAttackTime;
        public float EndAttackTime;
        public List<string> ColliderNames = new List<string>();
        public bool LaunchIntoAir;
        public bool MustCollide;
        public bool MustFaceAttacker;
        public float LethalRange;
        public int MaxHits;

        private List<AttackInfo> FinishedAttacks = new List<AttackInfo>();

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameter.Attack.ToString(), false);
            
            GameObject obj = PoolManager.Instance.GetObject(PoolObjectType.ATTACKINFO); 
            AttackInfo info = obj.GetComponent<AttackInfo>();

            obj.SetActive(true);
            info.ResetInfo(this, characterState.GetCharacterControl(animator));

            if (!AttackManager.Instance.CurrentAttacks.Contains(info))
            {
                AttackManager.Instance.CurrentAttacks.Add(info);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            RegisterAttack(characterState, animator, stateInfo);
            DeregisterAttack(characterState, animator, stateInfo);
            CheckCombo(characterState, animator, stateInfo);
        }

        public void RegisterAttack(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (StartAttackTime <= stateInfo.normalizedTime && EndAttackTime > stateInfo.normalizedTime)
            {
                foreach(AttackInfo info in AttackManager.Instance.CurrentAttacks)
                {
                    if (info == null)
                    {
                        continue;
                    }

                    if (!info.isRegisterd && info.AttackAbility == this)
                    {
                        if (debug)
                        {
                            Debug.Log(this.name + " registered: " + stateInfo.normalizedTime);
                        }
                        info.Register(this);
                    }
                }
            }
        }

        public void DeregisterAttack(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= EndAttackTime)
            {
                foreach(AttackInfo info in AttackManager.Instance.CurrentAttacks)
                {
                    if (info == null)
                    {
                        continue;
                    }

                    if (info.AttackAbility == this && !info.isFinished)
                    {
                        info.isFinished = true;
                        info.GetComponent<PoolObject>().TurnOff();

                        if (debug)
                        {
                            Debug.Log(this.name + " de-registered: " + stateInfo.normalizedTime);
                        }
                    }
                }
            }
        }

        public void CheckCombo(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= StartAttackTime + ((EndAttackTime - StartAttackTime) / 3f))
            {
                if (stateInfo.normalizedTime < EndAttackTime + ((EndAttackTime - StartAttackTime) / 2f))
                {
                    CharacterControl control = characterState.GetCharacterControl(animator);
                    if (control.Attack)
                    {
                        //Debug.Log("uppercut triggered");
                        animator.SetBool(TransitionParameter.Attack.ToString(), true);
                    }
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameter.Attack.ToString(), false);
            ClearAttack();
        }

        public void ClearAttack()
        {
            FinishedAttacks.Clear();

            foreach(AttackInfo info in AttackManager.Instance.CurrentAttacks)
            {
                if (info == null || info.AttackAbility == this /*info.isFinished*/)
                {
                    FinishedAttacks.Add(info);
                }
            }

            foreach(AttackInfo info in FinishedAttacks)
            {
                if (AttackManager.Instance.CurrentAttacks.Contains(info))
                {
                    AttackManager.Instance.CurrentAttacks.Remove(info);
                }
            }
        }
    }
}