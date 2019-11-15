using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AbilityData/MoveUp")]
    public class MoveUp : StateData
    {
        public AnimationCurve SpeedGraph;
        public float Speed;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!characterState.characterControl.RIGID_BODY.useGravity)
            {
                if (!UpIsBlocked(characterState.characterControl))
                {
                    characterState.characterControl.transform.
                    Translate(Vector3.up * Speed *
                    SpeedGraph.Evaluate(stateInfo.normalizedTime) *
                    Time.deltaTime);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        bool UpIsBlocked(CharacterControl control)
        {
            foreach (GameObject o in control.collisionSpheres.UpSpheres)
            {
                Debug.DrawRay(o.transform.position, control.transform.up * 0.3f, Color.yellow);

                RaycastHit hit;
                if (Physics.Raycast(o.transform.position, control.transform.up, out hit, 0.125f))
                {
                    if (hit.collider.transform.root.gameObject != control.gameObject &&
                        !Ledge.IsLedge(hit.collider.gameObject))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}