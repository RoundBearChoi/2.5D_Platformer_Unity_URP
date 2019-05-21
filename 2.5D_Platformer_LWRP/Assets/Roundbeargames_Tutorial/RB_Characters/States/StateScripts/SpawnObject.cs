using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/SpawnObject")]
    public class SpawnObject : StateData
    {
        [Range(0f, 1f)]
        public float SpawnTiming;
        public string ParentObjectName = string.Empty;

        private bool IsSpawned;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (SpawnTiming == 0f)
            {
                CharacterControl control = characterState.GetCharacterControl(animator);
                SpawnObj(control);
                IsSpawned = true;
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!IsSpawned)
            {
                if (stateInfo.normalizedTime >= SpawnTiming)
                {
                    CharacterControl control = characterState.GetCharacterControl(animator);
                    SpawnObj(control);
                    IsSpawned = true;
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            IsSpawned = false;
        }

        private void SpawnObj(CharacterControl control)
        {
            GameObject obj = PoolManager.Instance.GetObject(PoolObjectType.HAMMER);

            if (!string.IsNullOrEmpty(ParentObjectName))
            {
                GameObject p = control.GetChildObj(ParentObjectName);
                obj.transform.parent = p.transform;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
            }

            obj.SetActive(true);
        }
    }
}