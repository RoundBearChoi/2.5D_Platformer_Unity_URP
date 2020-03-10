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
        public MoveUp LatestMoveUp;
        private List<GameObject> FrontSpheresList;

        [Header("Attack Button")]
        public bool AttackTriggered;
        public bool AttackButtonIsReset;

        [Header("GroundMovement")]
        public bool disallowEarlyTurn;
        public bool LockDirectionNextState;
        public bool IsIgnoreCharacterTime;
        private float DirBlock;

        [Header("Colliding Objects")]
        public GameObject Ground;
        public Dictionary<TriggerDetector, List<Collider>> CollidingWeapons =
            new Dictionary<TriggerDetector, List<Collider>>();
        public Dictionary<TriggerDetector, List<Collider>> CollidingBodyParts =
            new Dictionary<TriggerDetector, List<Collider>>();

        public Dictionary<GameObject, GameObject> FrontBlockingObjs =
            new Dictionary<GameObject, GameObject>();
        public Dictionary<GameObject, GameObject> UpBlockingObjs =
            new Dictionary<GameObject, GameObject>();
        public Dictionary<GameObject, GameObject> DownBlockingObjs =
            new Dictionary<GameObject, GameObject>();

        [Header("AirControl")]
        public bool Jumped;
        public float AirMomentum;
        public bool CancelPull;
        public Vector3 MaxFallVelocity;
        public bool CanWallJump;
        public bool CheckWallBlock;
        public List<CharacterControl> MarioStompTargets = new List<CharacterControl>();
        public Attack MarioStompAttack;

        [Header("UpdateBoxCollider")]
        public bool UpdatingSpheres;
        public Vector3 TargetSize;
        public float Size_Speed;
        public Vector3 TargetCenter;
        public float Center_Speed;
        public Vector3 LandingPosition;
        public bool IsLanding;
        
        [Header("Transition")]
        public bool LockTransition;

        [Header("Weapon")]
        public MeleeWeapon HoldingWeapon;

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
                CheckFrontBlocking();
            }
            else
            {
                if (FrontBlockingObjs.Count != 0)
                {
                    FrontBlockingObjs.Clear();
                }
            }

            // checking while ledge grabbing
            if (IsRunning(typeof(MoveUp)))
            {
                if (LatestMoveUp.Speed > 0f)
                {
                    CheckUpBlocking();
                }
            }
            else
            {
                // checking while jumping up
                if (control.RIGID_BODY.velocity.y > 0.001f)
                {
                    CheckUpBlocking();

                    if (UpBlockingObjs.Count > 0)
                    {
                        control.RIGID_BODY.velocity = new Vector3(
                            control.RIGID_BODY.velocity.x,
                            0f,
                            control.RIGID_BODY.velocity.z);
                    }
                }
                else
                {
                    if (UpBlockingObjs.Count != 0)
                    {
                        UpBlockingObjs.Clear();
                    }
                }
            }

            CheckMarioStomp();
        }

        void CheckMarioStomp()
        {
            if (control.RIGID_BODY.velocity.y >= 0f)
            {
                MarioStompTargets.Clear();
                DownBlockingObjs.Clear();
                return;
            }

            if (MarioStompTargets.Count > 0)
            {
                control.RIGID_BODY.velocity = Vector3.zero;
                control.RIGID_BODY.AddForce(Vector3.up * 250f);

                foreach(CharacterControl c in MarioStompTargets)
                {
                    AttackInfo info = new AttackInfo();
                    info.CopyInfo(MarioStompAttack, control);

                    int index = Random.Range(0, c.RagdollParts.Count);
                    c.damageDetector.DamagedTrigger = c.RagdollParts[index].GetComponent<TriggerDetector>();
                    c.damageDetector.Attack = MarioStompAttack;
                    c.damageDetector.Attacker = control;
                    c.damageDetector.AttackingPart = control.RightFoot_Attack;

                    c.damageDetector.TakeDamage(info);
                }

                MarioStompTargets.Clear();
                return;
            }

            CheckDownBlocking();

            if (DownBlockingObjs.Count > 0)
            {
                foreach(KeyValuePair<GameObject, GameObject> data in DownBlockingObjs)
                {
                    CharacterControl c = CharacterManager.Instance.
                        GetCharacter(data.Value.transform.root.gameObject);

                    if (c != null)
                    {
                        if (c.boxCollider.center.y + c.transform.position.y < control.transform.position.y)
                        {
                            if (c != control)
                            {
                                if (!MarioStompTargets.Contains(c))
                                {
                                    MarioStompTargets.Add(c);
                                }
                            }
                        }
                    }
                }
            }
        }

        void CheckDownBlocking()
        {
            foreach (GameObject o in control.collisionSpheres.BottomSpheres)
            {
                CheckRaycastCollision(o, Vector3.down, 0.1f, DownBlockingObjs);
            }
        }

        void CheckUpBlocking()
        {
            foreach (GameObject o in control.collisionSpheres.UpSpheres)
            {
                CheckRaycastCollision(o, this.transform.up, 0.3f, UpBlockingObjs);
            }
        }

        public bool IsFacingAttacker()
        {
            Vector3 vec = control.damageDetector.Attacker.transform.position -
                control.transform.position;

            if (vec.z < 0f)
            {
                if (control.IsFacingForward())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (vec.z > 0f)
            {
                if (control.IsFacingForward())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        bool ForwardIsReversed()
        {
            if (LatestMoveForward.MoveOnHit)
            {
                if (IsFacingAttacker())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (LatestMoveForward.Speed > 0f)
            {
                return false;
            }
            else if (LatestMoveForward.Speed < 0f)
            {
                return true;
            }

            return false;
        }

        void CheckFrontBlocking()
        {
            if (!ForwardIsReversed())
            {
                FrontSpheresList = control.collisionSpheres.FrontSpheres;
                DirBlock = 1f;

                foreach(GameObject s in control.collisionSpheres.BackSpheres)
                {
                    if (FrontBlockingObjs.ContainsKey(s))
                    {
                        FrontBlockingObjs.Remove(s);
                    }
                }
            }
            else
            {
                FrontSpheresList = control.collisionSpheres.BackSpheres;
                DirBlock = -1f;

                foreach (GameObject s in control.collisionSpheres.FrontSpheres)
                {
                    if (FrontBlockingObjs.ContainsKey(s))
                    {
                        FrontBlockingObjs.Remove(s);
                    }
                }
            }

            foreach (GameObject o in FrontSpheresList)
            {
                CheckRaycastCollision(o, this.transform.forward * DirBlock, LatestMoveForward.BlockDistance,
                    FrontBlockingObjs);
            }
        }

        void CheckRaycastCollision(GameObject obj, Vector3 dir, float blockDistance,
            Dictionary<GameObject, GameObject> BlockingObjDic)
        {
            //Draw debug line
            Debug.DrawRay(obj.transform.position, dir * blockDistance, Color.yellow);

            //Check collision
            RaycastHit hit;
            if (Physics.Raycast(obj.transform.position, dir,
                out hit,
                blockDistance))
            {
                if (!IsBodyPart(hit.collider) &&
                    !IsIgnoringCharacter(hit.collider) &&
                    !Ledge.IsLedgeChecker(hit.collider.gameObject) &&
                    !MeleeWeapon.IsWeapon(hit.collider.gameObject) &&
                    !TrapSpikes.IsTrap(hit.collider.gameObject))
                {
                    if (BlockingObjDic.ContainsKey(obj))
                    {
                        BlockingObjDic[obj] = hit.collider.transform.root.gameObject;
                    }
                    else
                    {
                        BlockingObjDic.Add(obj, hit.collider.transform.root.gameObject);
                    }
                }
                else
                {
                    if (BlockingObjDic.ContainsKey(obj))
                    {
                        BlockingObjDic.Remove(obj);
                    }
                }
            }
            else
            {
                if (BlockingObjDic.ContainsKey(obj))
                {
                    BlockingObjDic.Remove(obj);
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

            if (target.damageDetector.IsDead())
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

        public bool StateNameContains(string str)
        {
            foreach (KeyValuePair<StateData, int> data in CurrentRunningAbilities)
            {
                if (data.Key.name.Contains(str))
                {
                    return true;
                }
            }

            return false;
        }

        public bool RightSideIsBlocked()
        {
            foreach(KeyValuePair<GameObject, GameObject> data in FrontBlockingObjs)
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
            foreach (KeyValuePair<GameObject, GameObject> data in FrontBlockingObjs)
            {
                if ((data.Value.transform.position - control.transform.position).z < 0f)
                {
                    return true;
                }
            }

            return false;
        }

        public MeleeWeapon GetTouchingWeapon()
        {
            foreach(KeyValuePair<TriggerDetector, List<Collider>> data in CollidingWeapons)
            {
                MeleeWeapon w = data.Value[0].gameObject.GetComponent<MeleeWeapon>();
                return w;
            }

            return null;
        }
    }
}