using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial_1
{
    public enum AbilityType
    {
        WALK,
    }

    public class WalkAbility : Ability
    {
        public override AbilityType abilityType
        {
            get
            {
                return AbilityType.WALK;
            }
        }

        public override void UpdateAbility(CharacterStateBase characterStateBase, Animator animator)
        {
            CharacterControl control = characterStateBase.GetCharacterControl(animator);

            if (control.MoveRight && control.MoveLeft)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                return;
            }

            if (!control.MoveRight && !control.MoveLeft)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
            }

            if (control.MoveRight)
            {
                control.FaceForward(true);
                control.transform.Translate(Vector3.forward * characterStateBase.moveData.WalkSpeed * Time.deltaTime);
                animator.SetBool(TransitionParameter.Move.ToString(), true);
            }

            if (control.MoveLeft)
            {
                control.FaceForward(false);
                control.transform.Translate(Vector3.forward * characterStateBase.moveData.WalkSpeed * Time.deltaTime);
                animator.SetBool(TransitionParameter.Move.ToString(), true);
            }
        }
    }
}