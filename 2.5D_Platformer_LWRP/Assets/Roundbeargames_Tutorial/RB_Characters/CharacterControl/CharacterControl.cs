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

        [Header("SubComponents")]
        public LedgeChecker ledgeChecker;
        public AnimationProgress animationProgress;
        public AIProgress aiProgress;
        public DamageDetector damageDetector;
        public CollisionSpheres collisionSpheres;
        public AIController aiController;
        public BoxCollider boxCollider;
        public NavMeshObstacle navMeshObstacle;

        [Header("Gravity")]
        public ContactPoint[] contactPoints;

        [Header("Setup")]
        public PlayableCharacterType playableCharacterType;
        public Animator SkinnedMeshAnimator;
        public List<Collider> RagdollParts = new List<Collider>();
        public GameObject LeftHand_Attack;
        public GameObject RightHand_Attack;
        public GameObject LeftFoot_Attack;
        public GameObject RightFoot_Attack;

        private List<TriggerDetector> TriggerDetectors = new List<TriggerDetector>();
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
            ledgeChecker = GetComponentInChildren<LedgeChecker>();
            animationProgress = GetComponent<AnimationProgress>();
            aiProgress = GetComponentInChildren<AIProgress>();
            damageDetector = GetComponentInChildren<DamageDetector>();
            aiController = GetComponentInChildren<AIController>();
            boxCollider = GetComponent<BoxCollider>();
            navMeshObstacle = GetComponent<NavMeshObstacle>();

            collisionSpheres = GetComponentInChildren<CollisionSpheres>();
            collisionSpheres.owner = this;
            collisionSpheres.SetColliderSpheres();
            
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

        public List<TriggerDetector> GetAllTriggers()
        {
            if (TriggerDetectors.Count == 0)
            {
                TriggerDetector[] arr = this.gameObject.GetComponentsInChildren<TriggerDetector>();

                foreach(TriggerDetector d in arr)
                {
                    TriggerDetectors.Add(d);
                }
            }

            return TriggerDetectors;
        }
        
        public void SetRagdollParts()
        {
            RagdollParts.Clear();

            Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();

            foreach(Collider c in colliders)
            {
                if (c.gameObject != this.gameObject)
                {
                    if (c.gameObject.GetComponent<LedgeChecker>() == null)
                    {
                        c.isTrigger = true;
                        RagdollParts.Add(c);
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

        public void TurnOnRagdoll()
        {
            //change layers
            Transform[] arr = GetComponentsInChildren<Transform>();
            foreach(Transform t in arr)
            {
                t.gameObject.layer = LayerMask.NameToLayer(RB_Layers.DEADBODY.ToString());
            }

            //save bodypart positions
            foreach (Collider c in RagdollParts)
            {
                TriggerDetector det = c.GetComponent<TriggerDetector>();
                det.LastPosition = c.gameObject.transform.localPosition;
                det.LastRotation = c.gameObject.transform.localRotation;
            }

            //turn off animator/avatar/etc
            RIGID_BODY.useGravity = false;
            RIGID_BODY.velocity = Vector3.zero;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            SkinnedMeshAnimator.enabled = false;
            SkinnedMeshAnimator.avatar = null;

            //turn on ragdoll
            foreach(Collider c in RagdollParts)
            {
                c.isTrigger = false;

                TriggerDetector det = c.GetComponent<TriggerDetector>();
                c.transform.localPosition = det.LastPosition;
                c.transform.localRotation = det.LastRotation;

                c.attachedRigidbody.velocity = Vector3.zero;
            }
        }

        public void UpdateBoxCollider_Size()
        {
            if (!animationProgress.UpdatingBoxCollider)
            {
                return;
            }

            if (Vector3.SqrMagnitude(boxCollider.size - animationProgress.TargetSize) > 0.01f)
            {
                boxCollider.size = Vector3.Lerp(boxCollider.size, animationProgress.TargetSize
                , Time.deltaTime * animationProgress.Size_Speed);

                animationProgress.UpdatingSpheres = true;
            }
        }

        public void UpdateBoxCollider_Center()
        {
            if (!animationProgress.UpdatingBoxCollider)
            {
                return;
            }

            if (Vector3.SqrMagnitude(boxCollider.center - animationProgress.TargetCenter) > 0.01f)
            {
                boxCollider.center = Vector3.Lerp(boxCollider.center, animationProgress.TargetCenter
                , Time.deltaTime * animationProgress.Center_Speed);

                animationProgress.UpdatingSpheres = true;
            }
        }

        private void FixedUpdate()
        {
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
            }

            if (animationProgress.RagdollTriggered)
            {
                TurnOnRagdoll();
                animationProgress.RagdollTriggered = false;
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
            foreach(Collider c in RagdollParts)
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
    }
}