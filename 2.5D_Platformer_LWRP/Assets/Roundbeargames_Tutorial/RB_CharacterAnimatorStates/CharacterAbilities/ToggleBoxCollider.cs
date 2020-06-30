using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/ToggleBoxCollider")]
    public class ToggleBoxCollider : CharacterAbility
    {
        public bool On;
        public bool OnStart;
        public bool OnEnd;
        [Space(10)]
        public bool RepositionSpheres;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (OnStart)
            {
                //CharacterControl control = characterState.GetCharacterControl(animator);
                ToggleBoxCol(characterState.characterControl);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (OnEnd)
            {
                //CharacterControl control = characterState.GetCharacterControl(animator);
                ToggleBoxCol(characterState.characterControl);
            }
        }

        private void ToggleBoxCol(CharacterControl control)
        {
            control.RIGID_BODY.velocity = Vector3.zero;
            control.GetComponent<BoxCollider>().enabled = On;

            if (RepositionSpheres)
            {
                control.COLLISION_SPHERE_DATA.Reposition_FrontSpheres();
                control.COLLISION_SPHERE_DATA.Reposition_BottomSpheres();
                control.COLLISION_SPHERE_DATA.Reposition_BackSpheres();
                control.COLLISION_SPHERE_DATA.Reposition_UpSpheres();
            }
        }
    }
}