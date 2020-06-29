using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class PlayerAttack : SubComponent
    {
        public AttackData attackData;

        private void Start()
        {
            attackData = new AttackData
            {
                AttackButtonIsReset = false,
                AttackTriggered = false,
            };

            subComponentProcessor.attackData = attackData;
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.PLAYER_ATTACK] = this;
            //subComponentProcessor.ComponentsDic.Add(SubComponentType.PLAYER_ATTACK, this);
        }

        public override void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate()
        {

            if (control.Attack)
            {
                if (attackData.AttackButtonIsReset)
                {
                    attackData.AttackTriggered = true;
                    attackData.AttackButtonIsReset = false;
                }
            }
            else
            {
                attackData.AttackButtonIsReset = true;
                attackData.AttackTriggered = false;
            }
        }
    }
}