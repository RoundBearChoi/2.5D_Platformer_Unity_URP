using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/Tools/AbilityListToArray")]
    public class AbilityListToArray : ScriptableObject
    {
        public AnimatorController TargetAnimator;

        public void Convert()
        {
            try
            {
                CharacterState[] arr = TargetAnimator.GetBehaviours<CharacterState>();

                foreach (CharacterState state in arr)
                {
                    if (state.ListAbilityData.Count != 0)
                    {
                        Debug.Log("List to array: " + state.name);
                        state.PutStatesInArray();
                    }
                }
            }
            catch(System.Exception e)
            {
                Debug.Log("Convert failed: " + e);
            }
        }

        public void ClearLists()
        {
            CharacterState[] arr = TargetAnimator.GetBehaviours<CharacterState>();

            foreach (CharacterState state in arr)
            {
                Debug.Log("Clearing list: " + state.name);
                state.ListAbilityData.Clear();
            }
        }
    }
}