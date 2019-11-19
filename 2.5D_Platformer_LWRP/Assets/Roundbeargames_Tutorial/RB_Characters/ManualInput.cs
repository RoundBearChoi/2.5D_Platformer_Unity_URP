using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ManualInput : MonoBehaviour
    {
        public List<InputKeyType> DoubleTaps = new List<InputKeyType>();

        private CharacterControl characterControl;
        private List<InputKeyType> UpKeys = new List<InputKeyType>();
        private Dictionary<InputKeyType, float> DicDoubleTapTimings = new Dictionary<InputKeyType, float>();

        private void Awake()
        {
            characterControl = this.gameObject.GetComponent<CharacterControl>();
        }

        void Update()
        {
            if (VirtualInputManager.Instance.Turbo)
            {
                characterControl.Turbo = true;
                ProcDoubleTap(InputKeyType.KEY_TURBO);
            }
            else
            {
                characterControl.Turbo = false;
                RemoveDoubleTap(InputKeyType.KEY_TURBO);
            }

            if (VirtualInputManager.Instance.MoveUp)
            {
                characterControl.MoveUp = true;
                ProcDoubleTap(InputKeyType.KEY_MOVE_UP);
            }
            else
            {
                characterControl.MoveUp = false;
                RemoveDoubleTap(InputKeyType.KEY_MOVE_UP);
            }

            if (VirtualInputManager.Instance.MoveDown)
            {
                characterControl.MoveDown = true;
                ProcDoubleTap(InputKeyType.KEY_MOVE_DOWN);
            }
            else
            {
                characterControl.MoveDown = false;
                RemoveDoubleTap(InputKeyType.KEY_MOVE_DOWN);
            }

            if (VirtualInputManager.Instance.MoveRight)
            {
                characterControl.MoveRight = true;
                ProcDoubleTap(InputKeyType.KEY_MOVE_RIGHT);
            }
            else
            {
                characterControl.MoveRight = false;
                RemoveDoubleTap(InputKeyType.KEY_MOVE_RIGHT);
            }

            if (VirtualInputManager.Instance.MoveLeft)
            {
                characterControl.MoveLeft = true;
                ProcDoubleTap(InputKeyType.KEY_MOVE_LEFT);
            }
            else
            {
                characterControl.MoveLeft = false;
                RemoveDoubleTap(InputKeyType.KEY_MOVE_LEFT);
            }

            if (VirtualInputManager.Instance.Jump)
            {
                characterControl.Jump = true;
                ProcDoubleTap(InputKeyType.KEY_JUMP);
            }
            else
            {
                characterControl.Jump = false;
                RemoveDoubleTap(InputKeyType.KEY_JUMP);
            }

            if (VirtualInputManager.Instance.Attack)
            {
                characterControl.Attack = true;
                ProcDoubleTap(InputKeyType.KEY_ATTACK);
            }
            else
            {
                characterControl.Attack = false;
                RemoveDoubleTap(InputKeyType.KEY_ATTACK);
            }

            //double tap running
            if (DoubleTaps.Contains(InputKeyType.KEY_MOVE_RIGHT) ||
                DoubleTaps.Contains(InputKeyType.KEY_MOVE_LEFT))
            {
                characterControl.Turbo = true;
            }

            //double tap running turn
            if (characterControl.MoveRight && characterControl.MoveLeft)
            {
                if (DoubleTaps.Contains(InputKeyType.KEY_MOVE_RIGHT) ||
                    DoubleTaps.Contains(InputKeyType.KEY_MOVE_LEFT))
                {
                    if (!DoubleTaps.Contains(InputKeyType.KEY_MOVE_RIGHT))
                    {
                        DoubleTaps.Add(InputKeyType.KEY_MOVE_RIGHT);
                    }

                    if (!DoubleTaps.Contains(InputKeyType.KEY_MOVE_LEFT))
                    {
                        DoubleTaps.Add(InputKeyType.KEY_MOVE_LEFT);
                    }
                }
            }
        }

        void ProcDoubleTap(InputKeyType keyType)
        {
            if (!DicDoubleTapTimings.ContainsKey(keyType))
            {
                DicDoubleTapTimings.Add(keyType, 0f);
            }

            if (DicDoubleTapTimings[keyType] == 0f ||
                UpKeys.Contains(keyType))
            {
                if (Time.time < DicDoubleTapTimings[keyType])
                {
                    if (!DoubleTaps.Contains(keyType))
                    {
                        DoubleTaps.Add(keyType);
                    }
                }

                if (UpKeys.Contains(keyType))
                {
                    UpKeys.Remove(keyType);
                }
                
                DicDoubleTapTimings[keyType] = Time.time + 0.18f;
            }
        }

        void RemoveDoubleTap(InputKeyType keyType)
        {
            if (DoubleTaps.Contains(keyType))
            {
                DoubleTaps.Remove(keyType);
            }

            if (!UpKeys.Contains(keyType))
            {
                UpKeys.Add(keyType);
            }
        }
    }
}