using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace Roundbeargames
{
    public static class TransitionConditionChecker
    {
        public static bool MakeTransition(CharacterControl control, List<TransitionConditionType> transitionConditions)
        {
            foreach (TransitionConditionType c in transitionConditions)
            {
                switch (c)
                {
                    case TransitionConditionType.UP:
                        {
                            if (!control.MoveUp)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.DOWN:
                        {
                            if (!control.MoveDown)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.LEFT:
                        {
                            if (!control.MoveLeft)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.RIGHT:
                        {
                            if (!control.MoveRight)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.ATTACK:
                        {
                            if (!control.ATTACK_DATA.AttackTriggered)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.JUMP:
                        {
                            if (!control.Jump)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.GRABBING_LEDGE:
                        {
                            if (!control.LEDGE_GRAB_DATA.isGrabbingLedge)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.NOT_GRABBING_LEDGE:
                        {
                            if (control.LEDGE_GRAB_DATA.isGrabbingLedge)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.LEFT_OR_RIGHT:
                        {
                            if (!control.MoveLeft && !control.MoveRight)
                            {
                                return false;
                            }

                            if (control.MoveLeft && control.MoveRight)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.GROUNDED:
                        {
                            if (control.SkinnedMeshAnimator.
                                GetBool(HashManager.Instance.ArrMainParams[
                                    (int)MainParameterType.Grounded]) == false)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.MOVE_FORWARD:
                        {
                            if (control.ROTATION_DATA.IsFacingForward())
                            {
                                if (!control.MoveRight)
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                if (!control.MoveLeft)
                                {
                                    return false;
                                }
                            }
                        }
                        break;
                    case TransitionConditionType.NOT_GROUNDED:
                        {
                            if (!control.SkinnedMeshAnimator.
                                GetBool(HashManager.Instance.ArrMainParams[
                                    (int)MainParameterType.Grounded]) == false)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.BLOCKED_BY_WALL:
                        {
                            for (int i = 0; i < control.COLLISION_SPHERE_DATA.FrontOverlapCheckers.Length; i++)
                            {
                                if (!control.COLLISION_SPHERE_DATA.FrontOverlapCheckers[i].ObjIsOverlapping)
                                {
                                    return false;
                                }
                            }

                            //foreach (OverlapChecker oc in control.COLLISION_SPHERE_DATA.FrontOverlapCheckers)
                            //{
                            //    if (!oc.ObjIsOverlapping)
                            //    {
                            //        return false;
                            //    }
                            //}
                        }
                        break;
                    case TransitionConditionType.NOT_BLOCKED_BY_WALL:
                        {
                            bool AllIsOverlapping = true;

                            for (int i = 0; i < control.COLLISION_SPHERE_DATA.FrontOverlapCheckers.Length; i++)
                            {
                                if (!control.COLLISION_SPHERE_DATA.FrontOverlapCheckers[i].ObjIsOverlapping)
                                {
                                    AllIsOverlapping = false;
                                }
                            }

                            //foreach (OverlapChecker oc in control.COLLISION_SPHERE_DATA.FrontOverlapCheckers)
                            //{
                            //    if (!oc.ObjIsOverlapping)
                            //    {
                            //        AllIsOverlapping = false;
                            //    }
                            //}

                            if (AllIsOverlapping)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.CAN_WALLJUMP:
                        {
                            if (!control.JUMP_DATA.CanWallJump)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.MOVING_TO_BLOCKING_OBJ:
                        {
                            List<GameObject> objs = control.BLOCKING_DATA.GetFrontBlockingObjList();

                            foreach (GameObject o in objs)
                            {
                                Vector3 dir = o.transform.position - control.transform.position;

                                if (dir.z > 0f && !control.MoveRight)
                                {
                                    return false;
                                }

                                if (dir.z < 0f && !control.MoveLeft)
                                {
                                    return false;
                                }
                            }
                        }
                        break;
                    case TransitionConditionType.DOUBLE_TAP_UP:
                        {
                            if (control.subComponentProcessor.ArrSubComponents[(int)SubComponentType.MANUALINPUT] == null)
                            {
                                return false;
                            }
                            //if (!control.subComponentProcessor.ComponentsDic.ContainsKey(SubComponentType.MANUALINPUT))
                            //{
                            //    return false;
                            //}

                            if (!control.MANUAL_INPUT_DATA.DoubleTapUp())
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.DOUBLE_TAP_DOWN:
                        {
                            if (control.subComponentProcessor.ArrSubComponents[(int)SubComponentType.MANUALINPUT] == null)
                            {
                                return false;
                            }
                            //if (!control.subComponentProcessor.ComponentsDic.ContainsKey(SubComponentType.MANUALINPUT))
                            //{
                            //    return false;
                            //}

                            if (!control.MANUAL_INPUT_DATA.DoubleTapDown())
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.DOUBLE_TAP_LEFT:
                        {
                            return false;
                        }
                        break;
                    case TransitionConditionType.DOUBLE_TAP_RIGHT:
                        {
                            return false;
                        }
                        break;
                    case TransitionConditionType.TOUCHING_WEAPON:
                        {
                            if (control.animationProgress.CollidingWeapons.Count == 0)
                            {
                                if (control.animationProgress.HoldingWeapon == null)
                                {
                                    return false;
                                }
                            }
                        }
                        break;
                    case TransitionConditionType.HOLDING_AXE:
                        {
                            if (control.animationProgress.HoldingWeapon == null)
                            {
                                return false;
                            }

                            if (!control.animationProgress.HoldingWeapon.name.Contains("Axe"))
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.NOT_MOVING:
                        {
                            if (control.MoveLeft || control.MoveRight)
                            {
                                if (!(control.MoveLeft && control.MoveRight))
                                {
                                    return false;
                                }
                            }
                        }
                        break;
                    case TransitionConditionType.RUN:
                        {
                            if (!control.Turbo)
                            {
                                return false;
                            }

                            if (control.MoveLeft && control.MoveRight)
                            {
                                return false;
                            }

                            if (!control.MoveLeft && !control.MoveRight)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.NOT_RUNNING:
                        {
                            if (control.Turbo)
                            {
                                if (control.MoveLeft || control.MoveRight)
                                {
                                    if (!(control.MoveLeft && control.MoveRight))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                        break;
                    case TransitionConditionType.BLOCKING:
                        {
                            if (!control.Block)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.NOT_BLOCKING:
                        {
                            if (control.Block)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.ATTACK_IS_BLOCKED:
                        {
                            if (control.DAMAGE_DATA.BlockedAttack == null)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.NOT_TURBO:
                        {
                            if (control.Turbo)
                            {
                                return false;
                            }
                        }
                        break;
                }
            }

            return true;
        }
    }
}