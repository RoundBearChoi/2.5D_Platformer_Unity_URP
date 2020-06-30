using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/GroundDetector")]
    public class GroundDetector : CharacterAbility
    {
        public float Distance;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (IsGrounded(characterState.characterControl))
            {
                animator.SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Grounded], true);
            }
            else
            {
                animator.SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Grounded], false);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        bool IsGrounded(CharacterControl control)
        {
            if (control.GROUND_DATA.BoxColliderContacts != null)
            {
                foreach (ContactPoint c in control.GROUND_DATA.BoxColliderContacts)
                {
                    float colliderBottom = (control.transform.position.y + control.boxCollider.center.y)
                        - (control.boxCollider.size.y / 2f);
                    float yDifference = Mathf.Abs(c.point.y - colliderBottom);

                    if (yDifference < 0.01f)
                    {
                        if (Mathf.Abs(control.RIGID_BODY.velocity.y) < 0.001f)
                        {
                            control.GROUND_DATA.Ground = c.otherCollider.transform.root.gameObject;
                            control.BOX_COLLIDER_DATA.LandingPosition = new Vector3(
                                0f,
                                c.point.y,
                                c.point.z);

                            return true;
                        }
                    }
                }
            }

            if (control.RIGID_BODY.velocity.y < 0f)
            {
                foreach (GameObject o in control.COLLISION_SPHERE_DATA.BottomSpheres)
                {
                    GameObject blockingObj = CollisionDetection.GetCollidingObject(control, o, -Vector3.up, Distance,
                        ref control.BLOCKING_DATA.RaycastContact);

                    if (blockingObj != null)
                    {
                        CharacterControl c = CharacterManager.Instance.GetCharacter(blockingObj.transform.root.gameObject);

                        if (c == null)
                        {
                            control.GROUND_DATA.Ground = blockingObj.transform.root.gameObject;
                            control.BOX_COLLIDER_DATA.LandingPosition = new Vector3(
                                0f,
                                control.BLOCKING_DATA.RaycastContact.y,
                                control.BLOCKING_DATA.RaycastContact.z);
                            return true;
                        }
                    }
                }
            }

            control.GROUND_DATA.Ground = null;
            return false;
        }
    }
}