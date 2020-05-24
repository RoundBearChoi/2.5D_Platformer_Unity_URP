using Roundbeargames.Datasets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    public enum TransitionParameter
    {
        Move,
        Jump,
        ForceTransition,
        Grounded,
        Attack,
        ClickAnimation,
        TransitionIndex,
        Turbo,
        Turn,
        LockTransition,
    }

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
        //public ManualInput manualInput;
        //public LedgeChecker ledgeChecker;
        public AnimationProgress animationProgress;
        public AIProgress aiProgress;
        public DamageDetector damageDetector;
        public CollisionSpheres collisionSpheres;
        public AIController aiController;
        public BoxCollider boxCollider;
        public NavMeshObstacle navMeshObstacle;
        public InstaKill instaKill;

        public DataProcessor dataProcessor;

        public BlockingObjData BLOCKING_DATA => subComponentProcessor.blockingData;
        public LedgeGrabData LEDGE_GRAB_DATA => subComponentProcessor.ledgeGrabData;
        public RagdollData RAGDOLL_DATA => subComponentProcessor.ragdollData;
        public ManualInputData MANUAL_INPUT_DATA => subComponentProcessor.manualInputData;
        public BoxColliderData BOX_COLLIDER_DATA => subComponentProcessor.boxColliderData;
        public DamageData DAMAGE_DATA => subComponentProcessor.damageData;
        public MomentumData MOMENTUM_DATA => subComponentProcessor.momentumData;
        public RotationData ROTATION_DATA => subComponentProcessor.rotationData;

        public Dataset AIR_CONTROL
        {
            get
            {
                return dataProcessor.GetDataset(typeof(AirControl));
            }
        }

        [Header("Gravity")]
        public ContactPoint[] contactPoints;

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
            //manualInput = GetComponent<ManualInput>();
            //ledgeChecker = GetComponentInChildren<LedgeChecker>();
            animationProgress = GetComponent<AnimationProgress>();
            aiProgress = GetComponentInChildren<AIProgress>();
            damageDetector = GetComponentInChildren<DamageDetector>();
            boxCollider = GetComponent<BoxCollider>();
            navMeshObstacle = GetComponent<NavMeshObstacle>();
            instaKill = GetComponentInChildren<InstaKill>();

            collisionSpheres = GetComponentInChildren<CollisionSpheres>();
            collisionSpheres.owner = this;
            collisionSpheres.SetColliderSpheres();

            dataProcessor = this.gameObject.GetComponentInChildren<DataProcessor>();
            System.Type[] arr = { 
                typeof(AirControl),
                typeof(SomeDataset)};
            dataProcessor.InitializeSets(arr);

            aiController = GetComponentInChildren<AIController>();
            if (aiController == null)
            {
                if (navMeshObstacle != null)
                {
                    navMeshObstacle.carving = true;
                }
            }

            RegisterCharacter();
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
            contactPoints = collision.contacts;
        }

        public void CacheCharacterControl(Animator animator)
        {
            CharacterState[] arr = animator.GetBehaviours<CharacterState>();

            foreach(CharacterState c in arr)
            {
                c.characterControl = this;
            }
        }

        private void RegisterCharacter()
        {
            if (!CharacterManager.Instance.Characters.Contains(this))
            {
                CharacterManager.Instance.Characters.Add(this);
            }
        }

        public void AddForceToDamagedPart(bool zeroVelocity)
        {
            if (DAMAGE_DATA.DamagedTrigger != null)
            {
                if (zeroVelocity)
                {
                    foreach (Collider c in RAGDOLL_DATA.BodyParts) 
                    {
                        c.attachedRigidbody.velocity = Vector3.zero;
                    }
                }

                DAMAGE_DATA.DamagedTrigger.GetComponent<Rigidbody>().
                    AddForce(DAMAGE_DATA.Attacker.transform.forward * DAMAGE_DATA.Attack.ForwardForce +
                    DAMAGE_DATA.Attacker.transform.right * DAMAGE_DATA.Attack.RightForce +
                    DAMAGE_DATA.Attacker.transform.up * DAMAGE_DATA.Attack.UpForce);
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