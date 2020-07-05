using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CollisionDetection : MonoBehaviour
    {
        public static GameObject GetCollidingObject(CharacterControl control, GameObject start, Vector3 dir,
            float blockDistance, ref Vector3 collisionPoint)
        {
            collisionPoint = Vector3.zero;

            //Draw debug line
            Debug.DrawRay(start.transform.position, dir * blockDistance, Color.yellow);

            //Check collision
            RaycastHit hit;
            if (Physics.Raycast(start.transform.position, dir,
                out hit,
                blockDistance))
            {
                if (!IsBodyPart(control, hit.collider) &&
                    !IsIgnoringCharacter(control, hit.collider) &&
                    !Ledge.IsLedgeChecker(hit.collider.gameObject) &&
                    !MeleeWeapon.IsWeapon(hit.collider.gameObject) &&
                    !TrapSpikes.IsTrap(hit.collider.gameObject))
                {
                    collisionPoint = hit.point;
                    return hit.collider.transform.root.gameObject;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        static bool IsIgnoringCharacter(CharacterControl control, Collider col)
        {
            if (!control.animationProgress.IsIgnoreCharacterTime)
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

        static bool IsBodyPart(CharacterControl control, Collider col)
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

            if (target.DAMAGE_DATA.IsDead())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}