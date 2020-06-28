using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CharacterState : StateMachineBehaviour
    {
        public CharacterControl characterControl;
        [Space(10)]
        public List<StateData> ListAbilityData = new List<StateData>();

        [SerializeField]
        private StateData[] States;

        public BlockingObjData BLOCKING_DATA => characterControl.subComponentProcessor.blockingData;
        public RagdollData RAGDOLL_DATA => characterControl.subComponentProcessor.ragdollData;
        public BoxColliderData BOX_COLLIDER_DATA => characterControl.subComponentProcessor.boxColliderData;
        public VerticalVelocityData VERTICAL_VELOCITY_DATA => characterControl.subComponentProcessor.verticalVelocityData;
        public MomentumData MOMENTUM_DATA => characterControl.subComponentProcessor.momentumData;
        public RotationData ROTATION_DATA => characterControl.subComponentProcessor.rotationData;
        public JumpData JUMP_DATA => characterControl.subComponentProcessor.jumpData;
        public CollisionSphereData COLLISION_SPHERE_DATA => characterControl.subComponentProcessor.collisionSphereData;
        public GroundData GROUND_DATA => characterControl.subComponentProcessor.groundData;
        public AttackData ATTACK_DATA => characterControl.subComponentProcessor.attackData;
        public AnimationData ANIMATION_DATA => characterControl.subComponentProcessor.animationData;
        public AIController AI_CONTROLLER => characterControl.aiController;

        public void PutStatesInArray()
        {
            States = new StateData[ListAbilityData.Count];

            for(int i = 0; i < ListAbilityData.Count; i++)
            {
                States[i] = ListAbilityData[i];
            }
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            for (int i = 0; i < States.Length; i++)
            {
                States[i].OnEnter(this, animator, stateInfo);

                if (characterControl.ANIMATION_DATA.CurrentRunningAbilities.ContainsKey(States[i]))
                {
                    characterControl.ANIMATION_DATA.CurrentRunningAbilities[States[i]] += 1;
                }
                else
                {
                    characterControl.ANIMATION_DATA.CurrentRunningAbilities.Add(States[i], 1);
                }
            }
        }

        public void UpdateAll(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            for (int i = 0; i < States.Length; i++)
            {
                States[i].UpdateAbility(characterState, animator, stateInfo);
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UpdateAll(this, animator, stateInfo);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            for (int i = 0; i < States.Length; i++)
            {
                try
                {
                    States[i].OnExit(this, animator, stateInfo);

                    if (characterControl.ANIMATION_DATA.CurrentRunningAbilities.ContainsKey(States[i]))
                    {
                        characterControl.ANIMATION_DATA.CurrentRunningAbilities[States[i]] -= 1;

                        if (characterControl.ANIMATION_DATA.CurrentRunningAbilities[States[i]] <= 0)
                        {
                            characterControl.ANIMATION_DATA.CurrentRunningAbilities.Remove(States[i]);
                        }
                    }
                }
                catch (System.Exception e)
                {
                    Debug.Log(e);
                }
            }
        }
    }
}