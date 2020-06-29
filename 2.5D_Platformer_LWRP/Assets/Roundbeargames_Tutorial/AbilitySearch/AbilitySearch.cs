using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilitySearch/AbilitySearch")]
    public class AbilitySearch : ScriptableObject
    {
        public AnimatorController ControllerToSearch;
        public StateData AbilityToSearch;
        public List<List<CharacterState>> Lists;

        public void Search()
        {
            CharacterState[] arr = ControllerToSearch.GetBehaviours<CharacterState>();

            Lists = new List<List<CharacterState>>();

            bool AbilityFound = false;
            int totalUses = 0;

            foreach (CharacterState state in arr)
            {
                foreach(StateData ability in state.ArrAbilities)
                {
                    if (ability == AbilityToSearch)
                    {
                        Debug.Log("---Ability Found---");
                        foreach(StateData a in state.ArrAbilities)
                        {
                            Debug.Log(a.name);
                        }

                        totalUses++;
                        AbilityFound = true;
                        break;
                    }
                }
            }

            if (!AbilityFound)
            {
                Debug.Log("Ability NOT found!");
            }
            else
            {
                Debug.Log("Total uses: " + totalUses);
            }
        }
    }
}