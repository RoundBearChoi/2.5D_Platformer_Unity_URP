using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoundBearGames_ObstacleCourse
{
    public class AbilityFactory : Singleton<AbilityFactory>
    {
        private Dictionary<AbilityType, Ability> DicAbilities = new Dictionary<AbilityType, Ability>();

        void Awake()
        {
            System.Type[] types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();

            foreach(System.Type t in types)
            {
                if (t.IsSubclassOf(typeof(Ability)))
                {
                    GameObject obj = new GameObject();

                    obj.AddComponent(t);
                    Ability a = obj.GetComponent<Ability>();
                    obj.name = a.abilityType.ToString();
                    DicAbilities.Add(a.abilityType, a);

                    obj.transform.parent = this.transform;
                    obj.transform.localPosition = Vector3.zero;
                    obj.transform.localRotation = Quaternion.identity;
                }
            }
        }

        public Ability GetAbility(AbilityType abilityType)
        {
            if (DicAbilities.ContainsKey(abilityType))
            {
                return DicAbilities[abilityType];
            }

            Debug.LogError(abilityType.ToString() + "not found in dictionary");

            return null;
        }
    }
}