using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    public enum RBScenes
    {
        TutorialScene_CharacterSelect,
        TutorialScene_Sample,
        TutorialScene_Sample_Night,
    }

    public class CharacterControl : MonoBehaviour
    {
        [Header("Input")]
        public bool Turbo;
        public bool MoveUp;
        public bool MoveDown;
        public bool MoveRight;
        public bool MoveLeft;
        public bool Jump;
        public bool Attack;
        public bool Block;

        [Header("SubComponents")]
        public SubComponentProcessor subComponentProcessor;

        // temp
        public AnimationProgress animationProgress;
        public AIProgress aiProgress;
        public AIController aiController;
        public BoxCollider boxCollider;
        public NavMeshObstacle navMeshObstacle;

        public BlockingObjData BLOCKING_DATA => subComponentProcessor.blockingData;
        public LedgeGrabData LEDGE_GRAB_DATA => subComponentProcessor.ledgeGrabData;
        public RagdollData RAGDOLL_DATA => subComponentProcessor.ragdollData;
        public ManualInputData MANUAL_INPUT_DATA => subComponentProcessor.manualInputData;
        public BoxColliderData BOX_COLLIDER_DATA => subComponentProcessor.boxColliderData;
        public DamageData DAMAGE_DATA => subComponentProcessor.damageData;
        public MomentumData MOMENTUM_DATA => subComponentProcessor.momentumData;
        public RotationData ROTATION_DATA => subComponentProcessor.rotationData;
        public JumpData JUMP_DATA => subComponentProcessor.jumpData;
        public CollisionSphereData COLLISION_SPHERE_DATA => subComponentProcessor.collisionSphereData;
        public InstaKillData INSTA_KILL_DATA => subComponentProcessor.instaKillData;
        public GroundData GROUND_DATA => subComponentProcessor.groundData;
        public AttackData ATTACK_DATA => subComponentProcessor.attackData;
        public AnimationData ANIMATION_DATA => subComponentProcessor.animationData;

        [Header("Setup")]
        public PlayableCharacterType playableCharacterType;
        public Animator SkinnedMeshAnimator;
        public GameObject LeftHand_Attack;
        public GameObject RightHand_Attack;
        public GameObject LeftFoot_Attack;
        public GameObject RightFoot_Attack;
        
        private Dictionary<string, GameObject> ChildObjects = new Dictionary<string, GameObject>();

        private Rigidbody rigid;

        public Rigidbody RIGID_BODY
        {
            get
            {
                if (rigid == null)
                {
                    rigid = GetComponent<Rigidbody>();
                }
                return rigid;
            }
        }

        private void Awake()
        {
            subComponentProcessor = GetComponentInChildren<SubComponentProcessor>();

            // temp
            animationProgress = GetComponent<AnimationProgress>();
            aiProgress = GetComponentInChildren<AIProgress>();
            boxCollider = GetComponent<BoxCollider>();
            navMeshObstacle = GetComponent<NavMeshObstacle>();

            aiController = GetComponentInChildren<AIController>();
            if (aiController == null)
            {
                if (navMeshObstacle != null)
                {
                    navMeshObstacle.carving = true;
                }
            }

            RegisterCharacter();
            InitCharacterStates(SkinnedMeshAnimator);
        }


        private void Update()
        {
            subComponentProcessor.UpdateSubComponents();
        }

        private void FixedUpdate()
        {
            subComponentProcessor.FixedUpdateSubComponents();
        }

        private void OnCollisionStay(Collision collision)
        {
            GROUND_DATA.BoxColliderContacts = collision.contacts;
        }

        void InitCharacterStates(Animator animator)
        {
            CharacterState[] arr = animator.GetBehaviours<CharacterState>();

            foreach(CharacterState c in arr)
            {
                c.characterControl = this;
                //c.PutStatesInArray();
            }
        }

        void RegisterCharacter()
        {
            if (!CharacterManager.Instance.Characters.Contains(this))
            {
                CharacterManager.Instance.Characters.Add(this);
            }
        }

        public void MoveForward(float Speed, float SpeedGraph)
        {
            transform.Translate(Vector3.forward * Speed * SpeedGraph * Time.deltaTime);
        }

        public GameObject GetChildObj(string name)
        {
            if (ChildObjects.ContainsKey(name))
            {
                return ChildObjects[name];
            }

            Transform[] arr = this.gameObject.GetComponentsInChildren<Transform>();

            foreach(Transform t in arr)
            {
                if (t.gameObject.name.Equals(name))
                {
                    ChildObjects.Add(name, t.gameObject);
                    return t.gameObject;
                }
            }

            return null;
        }

        public GameObject GetAttackingPart(AttackPartType attackPart)
        {
            if (attackPart == AttackPartType.LEFT_HAND)
            {
                return LeftHand_Attack;
            }
            else if (attackPart == AttackPartType.RIGHT_HAND)
            {
                return RightHand_Attack;
            }
            else if (attackPart == AttackPartType.LEFT_FOOT)
            {
                return LeftFoot_Attack;
            }
            else if (attackPart == AttackPartType.RIGHT_FOOT)
            {
                return RightFoot_Attack;
            }
            else if (attackPart == AttackPartType.MELEE_WEAPON)
            {
                return animationProgress.HoldingWeapon.triggerDetector.gameObject;
            }

            return null;
        }
    }
}