using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class AnimationProgress : MonoBehaviour
    {
        public Dictionary<StateData, int> CurrentRunningAbilities =
            new Dictionary<StateData, int>();

        public bool CameraShaken;
        public List<PoolObjectType> SpawnedObjList = new List<PoolObjectType>();
        public bool RagdollTriggered;
        public MoveForward LatestMoveForward;

        [Header("Attack Button")]
        public bool AttackTriggered;
        public bool AttackButtonIsReset;

        [Header("GroundMovement")]
        public bool disallowEarlyTurn;
        public bool LockDirectionNextState;
        public bool IsIgnoreCharacterTime;
        private List<GameObject> SpheresList;
        private float DirBlock;

        [Header("Colliding Objects")]
        public GameObject Ground;
        public Dictionary<TriggerDetector, List<Collider>> CollidingBodyParts =
            new Dictionary<TriggerDetector, List<Collider>>();
        public Dictionary<GameObject, GameObject> BlockingObjs =
            new Dictionary<GameObject, GameObject>();

        [Header("AirControl")]
        public bool Jumped;
        public float AirMomentum;
        public bool CancelPull;
        public Vector3 MaxFallVelocity;
        public bool CanWallJump;
        public bool CheckWallBlock;

        [Header("UpdateBoxCollider")]
        public bool UpdatingSpheres;
        public Vector3 TargetSize;
        public float Size_Speed;
        public Vector3 TargetCenter;
        public float Center_Speed;

        [Header("Damage Info")]
        public Attack Attack;
        public CharacterControl Attacker;
        public TriggerDetector DamagedTrigger;
        public GameObject AttackingPart;

        [Header("Transition")]
        public bool LockTransition;

        private CharacterControl control;

        private void Awake()
        {
            control = GetComponent<CharacterControl>();
        }

        private void Update()
        {
            if (control.Attack)
            {
                if (AttackButtonIsReset)
                {
                    AttackTriggered = true;
                    AttackButtonIsReset = false;
                }
            }
            else
            {
                AttackButtonIsReset = true;
                AttackTriggered = false;
            }

            if (IsRunning(typeof(LockTransition)))
            {
                if (control.animationProgress.LockTransition)
                {
                    control.SkinnedMeshAnimator.
                        SetBool(HashManager.Instance.DicMainParams[TransitionParameter.LockTransition],
                        true);
                }
                else
                {
                    control.SkinnedMeshAnimator.
                        SetBool(HashManager.Instance.DicMainParams[TransitionParameter.LockTransition],
                        false);
                }
            }
            else
            {
                control.SkinnedMeshAnimator.
                    SetBool(HashManager.Instance.DicMainParams[TransitionParameter.LockTransition],
                    false);
            }
        }

        private void FixedUpdate()
        {
            if (IsRunning(typeof(MoveForward)))
            {
                CheckBlockingObjs();
            }
            else
            {
                if (BlockingObjs.Count != 0)
                {
                    BlockingObjs.Clear();
                }
            }
        }

        void CheckBlockingObjs()
        {
            if (LatestMoveForward.Speed > 0)
            {
                SpheresList = control.collisionSpheres.FrontSpheres;
                DirBlock = 0.3f;

                foreach(GameObject s in control.collisionSpheres.BackSpheres)
                {
                    if (BlockingObjs.ContainsKey(s))
                    {
                        BlockingObjs.Remove(s);
                    }
                }
            }
            else
            {
                SpheresList = control.collisionSpheres.BackSpheres;
                DirBlock = -0.3f;

                foreach (GameObject s in control.collisionSpheres.FrontSpheres)
                {
                    if (BlockingObjs.ContainsKey(s))
                    {
                        BlockingObjs.Remove(s);
                    }
                }
            }

            foreach (GameObject o in SpheresList)
            {
                Debug.DrawRay(o.transform.position, control.transform.forward * DirBlock, Color.yellow);
                RaycastHit hit;
                if (Physics.Raycast(o.transform.position, control.transform.forward * DirBlock,
                    out hit,
                    LatestMoveForward.BlockDistance))
                {
                    if (!IsBodyPart(hit.collider) &&
                        !IsIgnoringCharacter(hit.collider) &&
                        !Ledge.IsLedge(hit.collider.gameObject) &&
                        !Ledge.IsLedgeChecker(hit.collider.gameObject))
                    {
                        if (BlockingObjs.ContainsKey(o))
                        {
                            BlockingObjs[o] = hit.collider.transform.root.gameObject;
                        }
                        else
                        {
                            BlockingObjs.Add(o, hit.collider.transform.root.gameObject);
                        }
                    }
                    else
                    {
                        if (BlockingObjs.ContainsKey(o))
                        {
                            BlockingObjs.Remove(o);
                        }
                    }
                }
                else
                {
                    if (BlockingObjs.ContainsKey(o))
                    {
                        BlockingObjs.Remove(o);
                    }
                }
            }
        }

        bool IsIgnoringCharacter(Collider col)
        {
            if (!IsIgnoreCharacterTime)
            {
                return false;
            }
            else
            {
                CharacterControl blockingChar = CharacterManager.Instance.GetCharacter(col.transform.root.gameObject);

                if (blockingChar == null)
                {
                    return false;
                }

                if (blockingChar == control)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        bool IsBodyPart(Collider col)
        {
            if (col.transform.root.gameObject == control.gameObject)
            {
                return true;
            }

            CharacterControl target = CharacterManager.Instance.GetCharacter(col.transform.root.gameObject);

            if (target == null)
            {
                return false;
            }

            if (target.damageDetector.DamageTaken > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsRunning(System.Type type)
        {
            foreach(KeyValuePair<StateData, int> data in CurrentRunningAbilities)
            {
                if (data.Key.GetType() == type)
                {
                    return true;
                }
            }

            return false;
        }

        public bool RightSideIsBlocked()
        {
            foreach(KeyValuePair<GameObject, GameObject> data in BlockingObjs)
            {
                if ((data.Value.transform.position - control.transform.position).z > 0f)
                {
                    return true;
                }
            }

            return false;
        }

        public bool LeftSideIsBlocked()
        {
            foreach (KeyValuePair<GameObject, GameObject> data in BlockingObjs)
            {
                if ((data.Value.transform.position - control.transform.position).z < 0f)
                {
                    return true;
                }
            }

            return false;
        }
    }
}