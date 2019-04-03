using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/MoveForward")]
    public class MoveForward : StateData
    {
        public float Speed;

        public override void UpdateAbility(CharacterState characterState, Animator animator)
        {
            CharacterControl c = characterState.GetCharacterControl(animator);

            if (VirtualInputManager.Instance.MoveRight && VirtualInputManager.Instance.MoveLeft)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                return;
            }

            if (!VirtualInputManager.Instance.MoveRight && !VirtualInputManager.Instance.MoveLeft)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                return;
            }

            if (VirtualInputManager.Instance.MoveRight)
            {
                c.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                c.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }

            if (VirtualInputManager.Instance.MoveLeft)
            {
                c.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                c.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }
    }
}