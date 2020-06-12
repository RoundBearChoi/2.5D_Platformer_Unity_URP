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

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (characterControl == null)
            {
                characterControl = animator.transform.root.GetComponent<CharacterControl>();
                characterControl.CacheCharacterControl(animator);
            }

            foreach(StateData d in ListAbilityData)
            {
                d.OnEnter(this, animator, stateInfo);

                if (characterControl.ANIMATION_DATA.CurrentRunningAbilities.ContainsKey(d))
                {
                    characterControl.ANIMATION_DATA.CurrentRunningAbilities[d] += 1;
                }
                else
                {
                    characterControl.ANIMATION_DATA.CurrentRunningAbilities.Add(d, 1);
                }
            }
        }

        public void UpdateAll(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            foreach(StateData d in ListAbilityData)
            {
                d.UpdateAbility(characterState, animator, stateInfo);
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UpdateAll(this, animator, stateInfo);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (StateData d in ListAbilityData)
            {
                d.OnExit(this, animator, stateInfo);

                if (characterControl.ANIMATION_DATA.CurrentRunningAbilities.ContainsKey(d))
                {
                    characterControl.ANIMATION_DATA.CurrentRunningAbilities[d] -= 1;

                    if (characterControl.ANIMATION_DATA.CurrentRunningAbilities[d] <= 0)
                    {
                        characterControl.ANIMATION_DATA.CurrentRunningAbilities.Remove(d);
                    }
                }
            }
        }
    }
}