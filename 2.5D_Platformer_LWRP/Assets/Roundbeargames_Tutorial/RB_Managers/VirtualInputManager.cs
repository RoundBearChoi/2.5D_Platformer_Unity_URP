using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum InputKeyType
    {
        KEY_MOVE_UP,
        KEY_MOVE_DOWN,
        KEY_MOVE_RIGHT,
        KEY_MOVE_LEFT,

        KEY_JUMP,
        KEY_ATTACK,
        KEY_TURBO,
    }

    public class VirtualInputManager : Singleton<VirtualInputManager>
    {
        public PlayerInput playerInput;
        public bool Turbo;
        public bool MoveUp;
        public bool MoveDown;
        public bool MoveRight;
        public bool MoveLeft;
        public bool Jump;
        public bool Attack;

        [Header("Custom Key Binding")]
        public bool UseCustomKeys;
        [Space(5)]
        public bool Bind_MoveUp;
        public bool Bind_MoveDown;
        public bool Bind_MoveRight;
        public bool Bind_MoveLeft;
        public bool Bind_Jump;
        public bool Bind_Attack;
        public bool Bind_Turbo;

        [Space(10)]
        public Dictionary<InputKeyType, KeyCode> DicKeys = new Dictionary<InputKeyType, KeyCode>();
        public KeyCode[] PossibleKeys;

        private void Awake()
        {
            PossibleKeys = System.Enum.GetValues(typeof(KeyCode)) as KeyCode[];

            GameObject obj = Instantiate(Resources.Load("PlayerInput", typeof(GameObject))) as GameObject;
            playerInput = obj.GetComponent<PlayerInput>();
        }

        public void LoadKeys()
        {
            if (playerInput.savedKeys.KeyCodesList.Count > 0)
            {
                foreach(KeyCode k in playerInput.savedKeys.KeyCodesList)
                {
                    if (k == KeyCode.None)
                    {
                        SetDefaultKeys();
                        break;
                    }
                }
            }
            else
            {
                SetDefaultKeys();
            }

            for (int i = 0; i < playerInput.savedKeys.KeyCodesList.Count; i++)
            {
                DicKeys[(InputKeyType)i] = playerInput.savedKeys.KeyCodesList[i];
            }
        }

        public void SaveKeys()
        {
            playerInput.savedKeys.KeyCodesList.Clear();

            int count = System.Enum.GetValues(typeof(InputKeyType)).Length;

            for (int i = 0; i < count; i++)
            {
                playerInput.savedKeys.KeyCodesList.Add(DicKeys[(InputKeyType)i]);
            }
        }

        public void SetDefaultKeys()
        {
            DicKeys.Clear();

            DicKeys.Add(InputKeyType.KEY_MOVE_UP,       KeyCode.W);
            DicKeys.Add(InputKeyType.KEY_MOVE_DOWN,     KeyCode.S);
            DicKeys.Add(InputKeyType.KEY_MOVE_LEFT,     KeyCode.A);
            DicKeys.Add(InputKeyType.KEY_MOVE_RIGHT,    KeyCode.D);

            DicKeys.Add(InputKeyType.KEY_JUMP,          KeyCode.Space);
            DicKeys.Add(InputKeyType.KEY_ATTACK,        KeyCode.Return);
            DicKeys.Add(InputKeyType.KEY_TURBO,         KeyCode.LeftShift);

            SaveKeys();
        }

        private void Update()
        {
            //if (!UseCustomKeys)
            //{
            //    return;
            //}

            if (UseCustomKeys)
            {
                if (Bind_MoveUp)
                {
                    if (KeyIsChanged(InputKeyType.KEY_MOVE_UP))
                    {
                        Bind_MoveUp = false;
                    }
                }
                
                if (Bind_MoveDown)
                {
                    if (KeyIsChanged(InputKeyType.KEY_MOVE_DOWN))
                    {
                        Bind_MoveDown = false;
                    }
                }
                
                if (Bind_MoveRight)
                {
                    if (KeyIsChanged(InputKeyType.KEY_MOVE_RIGHT))
                    {
                        Bind_MoveRight = false;
                    }
                }
                
                if (Bind_MoveLeft)
                {
                    if (KeyIsChanged(InputKeyType.KEY_MOVE_LEFT))
                    {
                        Bind_MoveLeft = false;
                    }
                }
                
                if (Bind_Jump)
                {
                    if (KeyIsChanged(InputKeyType.KEY_JUMP))
                    {
                        Bind_Jump = false;
                    }
                }
                
                if (Bind_Attack)
                {
                    if (KeyIsChanged(InputKeyType.KEY_ATTACK))
                    {
                        Bind_Attack = false;
                    }
                }

                if (Bind_Turbo)
                {
                    if (KeyIsChanged(InputKeyType.KEY_TURBO))
                    {
                        Bind_Turbo = false;
                    }
                }
            }
        }

        void SetCustomKey(InputKeyType inputKey, KeyCode key)
        {
            Debug.Log("key changed: " + inputKey.ToString() + " -> " + key.ToString());

            if (!DicKeys.ContainsKey(inputKey))
            {
                DicKeys.Add(inputKey, key);
            }
            else
            {
                DicKeys[inputKey] = key;
            }

            SaveKeys();
        }

        bool KeyIsChanged(InputKeyType inputKey)
        {
            if (Input.anyKey)
            {
                foreach(KeyCode k in PossibleKeys)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        continue;
                    }

                    if (Input.GetKeyDown(k))
                    {
                        SetCustomKey(inputKey, k);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
