using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class PlayerSpawn : MonoBehaviour
    {
        public CharacterSelect characterSelect;

        private string objName;

        private void Start()
        {
            switch (characterSelect.SelectedCharacterType)
            {
                case PlayableCharacterType.YELLOW:
                    {
                        objName = "yBot - Yellow";
                    }
                    break;
                case PlayableCharacterType.RED:
                    {
                        objName = "yBot - Red Variant";
                    }
                    break;
                case PlayableCharacterType.GREEN:
                    {
                        objName = "yBot - Green Variant";
                    }
                    break;
            }

            GameObject obj = Instantiate(Resources.Load(objName, typeof(GameObject))) as GameObject;
            obj.transform.position = this.transform.position;
            GetComponent<MeshRenderer>().enabled = false;

            Cinemachine.CinemachineVirtualCamera[] arr = GameObject.FindObjectsOfType<Cinemachine.CinemachineVirtualCamera>();
            foreach(Cinemachine.CinemachineVirtualCamera v in arr)
            {
                CharacterControl control = CharacterManager.Instance.GetCharacter(characterSelect.SelectedCharacterType);
                Collider target = control.GetBodyPart("Spine1");

                v.LookAt = target.transform;
                v.Follow = target.transform;
            }
        }
    }
}