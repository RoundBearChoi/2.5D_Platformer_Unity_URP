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
        public Dictionary<SubComponents, SubComponent> SubComponentsDic = new Dictionary<SubComponents, SubComponent>();

        public Dictionary<BoolData, GetBool> BoolDic = new Dictionary<BoolData, GetBool>();
        public delegate bool GetBool();

        public Dictionary<ListData, GetList> ListDic = new Dictionary<ListData, GetList>();
        public delegate List<GameObject> GetList();

        public Dictionary<CharacterProc, CharacterProcDel> ProcDic = new Dictionary<CharacterProc, CharacterProcDel>();
        public delegate void CharacterProcDel();

        [Header("Gravity")]
        public ContactPoint[] contactPoints;

        [Header("Setup")]
        public PlayableCharacterType playableCharacterType;
        public Animator SkinnedMeshAnimator;
        public List<Collider> BodyParts = new List<Collider>();
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

        public void CacheCharacterControl(Animator animator)
        {
            CharacterState[] arr = animator.GetBehaviours<CharacterState>();

            foreach(CharacterState c in arr)
            {
                c.characterControl = this;
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            contactPoints = collision.contacts;
        }

        private void RegisterCharacter()
        {
            if (!CharacterManager.Instance.Characters.Contains(this))
            {
                CharacterManager.Instance.Characters.Add(this);
            }
        }
        
        public void SetupBodyParts()
        {
            BodyParts.Clear();

            Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();

            foreach(Collider c in colliders)
            {
                if (c.gameObject != this.gameObject)
                {
                    if (c.gameObject.GetComponent<LedgeChecker>() == null &&
                        c.gameObject.GetComponent<LedgeCollider>() == null)
                    {
                        c.isTrigger = true;
                        BodyParts.Add(c);
                        c.attachedRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
                        c.attachedRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

                        CharacterJoint joint = c.GetComponent<CharacterJoint>();
                        if (joint != null)
                        {
                            joint.enableProjection = true;
                        }

                        if (c.GetComponent<TriggerDetector>() == null)
                        {
                            c.gameObject.AddComponent<TriggerDetector>();
                        }
                    }
                }
            }
        }

        public void AddForceToDamagedPart(bool zeroVelocity)
        {
            if (damageDetector.DamagedTrigger != null)
            {
                if (zeroVelocity)
                {
                    foreach (Collider c in BodyParts)
                    {
                        c.attachedRigidbody.velocity = Vector3.zero;
                    }
                }

                damageDetector.DamagedTrigger.GetComponent<Rigidbody>().
                    AddForce(damageDetector.Attacker.transform.forward * damageDetector.Attack.ForwardForce +
                    damageDetector.Attacker.transform.right * damageDetector.Attack.RightForce +
                    damageDetector.Attacker.transform.up * damageDetector.Attack.UpForce);
            }
        }

        public void UpdateBoxCollider_Size()
        {
            if (!animationProgress.IsRunning(typeof(UpdateBoxCollider)))
            {
                return;
            }

            if (Vector3.SqrMagnitude(boxCollider.size - animationProgress.TargetSize) > 0.00001f)
            {
                boxCollider.size = Vector3.Lerp(boxCollider.size,
                    animationProgress.TargetSize,
                    Time.deltaTime * animationProgress.Size_Speed);

                animationProgress.UpdatingSpheres = true;
            }
        }

        public void UpdateBoxCollider_Center()
        {
            if (!animationProgress.IsRunning(typeof(UpdateBoxCollider)))
            {
                return;
            }

            if (Vector3.SqrMagnitude(boxCollider.center - animationProgress.TargetCenter) > 0.00001f)
            {
                boxCollider.center = Vector3.Lerp(boxCollider.center,
                    animationProgress.TargetCenter,
                    Time.deltaTime * animationProgress.Center_Speed);

                animationProgress.UpdatingSpheres = true;
            }
        }

        void UpdateSubComponent(SubComponents type)
        {
            if (SubComponentsDic.ContainsKey(type))
            {
                SubComponentsDic[type].OnUpdate();
            }
        }

        void FixedUpdateSubComponent(SubComponents type)
        {
            if (SubComponentsDic.ContainsKey(type))
            {
                SubComponentsDic[type].OnFixedUpdate();
            }
        }

        private void Update()
        {
            UpdateSubComponent(SubComponents.MANUALINPUT);
        }

        private void FixedUpdate()
        {
            FixedUpdateSubComponent(SubComponents.LEDGECHECKER);
            FixedUpdateSubComponent(SubComponents.RAGDOLL);
            FixedUpdateSubComponent(SubComponents.BLOCKINGOBJECTS);

            if (!animationProgress.CancelPull)
            {
                if (RIGID_BODY.velocity.y > 0f && !Jump)
                {
                    RIGID_BODY.velocity -= (Vector3.up * RIGID_BODY.velocity.y * 0.1f);
                }
            }

            animationProgress.UpdatingSpheres = false;
            UpdateBoxCollider_Size();
            UpdateBoxCollider_Center();
            if (animationProgress.UpdatingSpheres)
            {
                collisionSpheres.Reposition_FrontSpheres();
                collisionSpheres.Reposition_BottomSpheres();
                collisionSpheres.Reposition_BackSpheres();
                collisionSpheres.Reposition_UpSpheres();

                if (animationProgress.IsLanding)
                {
                    //Debug.Log("repositioning y");
                    RIGID_BODY.MovePosition(new Vector3(
                        0f,
                        animationProgress.LandingPosition.y,
                        this.transform.position.z));
                }
            }

            //slow down wallslide
            if (animationProgress.MaxFallVelocity.y != 0f)
            {
                if (RIGID_BODY.velocity.y <= animationProgress.MaxFallVelocity.y)
                {
                    RIGID_BODY.velocity = animationProgress.MaxFallVelocity;
                }
            }
        }
        
        public void MoveForward(float Speed, float SpeedGraph)
        {
            transform.Translate(Vector3.forward * Speed * SpeedGraph * Time.deltaTime);
        }

        public void FaceForward(bool forward)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals(RBScenes.TutorialScene_CharacterSelect.ToString()))
            {
                return;
            }

            if (!SkinnedMeshAnimator.enabled)
            {
                return;
            }

            if (forward)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }

        public bool IsFacingForward()
        {
            if (transform.forward.z > 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Collider GetBodyPart(string name)
        {
            foreach(Collider c in BodyParts)
            {
                if (c.name.Contains(name))
                {
                    return c;
                }
            }

            return null;
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