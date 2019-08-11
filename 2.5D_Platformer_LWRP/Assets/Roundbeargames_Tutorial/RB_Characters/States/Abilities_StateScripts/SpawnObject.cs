using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/SpawnObject")]
    public class SpawnObject : StateData
    {
        public PoolObjectType ObjectType;
        [Range(0f, 1f)]
        public float SpawnTiming;
        public string ParentObjectName = string.Empty;
        public bool StickToParent;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (SpawnTiming == 0f)
            {
                //CharacterControl control = characterState.GetCharacterControl(animator);
                SpawnObj(characterState.characterControl);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //CharacterControl control = characterState.GetCharacterControl(animator);

            if (!characterState.characterControl.animationProgress.SpawnedObjList.Contains(ObjectType))
            {
                if (stateInfo.normalizedTime >= SpawnTiming)
                {
                    SpawnObj(characterState.characterControl);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //CharacterControl control = characterState.GetCharacterControl(animator);
            if (characterState.characterControl.animationProgress.SpawnedObjList.Contains(ObjectType))
            {
                characterState.characterControl.animationProgress.SpawnedObjList.Remove(ObjectType);
            }
        }

        private void SpawnObj(CharacterControl control)
        {
            if (control.animationProgress.SpawnedObjList.Contains(ObjectType))
            {
                return;
            }

            GameObject obj = PoolManager.Instance.GetObject(ObjectType);

            Debug.Log("spawning " + ObjectType.ToString() + " | looking for: " + ParentObjectName);

            if (!string.IsNullOrEmpty(ParentObjectName))
            {
                GameObject p = control.GetChildObj(ParentObjectName);
                obj.transform.parent = p.transform;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
            }

            if (!StickToParent)
            {
                obj.transform.parent = null;
            }

            obj.SetActive(true);

            control.animationProgress.SpawnedObjList.Add(ObjectType);
        }
    }
}