using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoundBearGames_ObstacleCourse
{
    public abstract class CharacterStateBase : StateMachineBehaviour
    {
        public AnimationData animationData;
        public MoveData moveData;
        public AttackData attackData;

        //[SerializeField]
        private List<Ability> ListAbilities = new List<Ability>();
        public void AddAbility(Ability ability)
        {
            if (!ListAbilities.Contains(ability))
            {
                ListAbilities.Add(ability);
            }
        }

        public void UpdateAbilities(CharacterStateBase characterStateBase, Animator animator)
        {
            foreach(Ability a in ListAbilities)
            {
                a.UpdateAbility(characterStateBase, animator);
            }
        }

        private CharacterControl characterControl;
        public CharacterControl GetCharacterControl(Animator animator)
        {
            if (characterControl == null)
            {
                characterControl = animator.GetComponentInParent<CharacterControl>();
            }
            return characterControl;
        }
    }
}