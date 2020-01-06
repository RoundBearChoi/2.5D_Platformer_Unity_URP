using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class TrapSpikes : MonoBehaviour
    {
        public List<CharacterControl> ListCharacters = new List<CharacterControl>();
        public List<CharacterControl> ListSpikeVictims = new List<CharacterControl>();
        public List<Spike> ListSpikes = new List<Spike>();
        public RuntimeAnimatorController SpikeDeathAnimator;

        Coroutine SpikeTriggerRoutine;
        bool SpikesReloaded;

        private void Start()
        {
            SpikeTriggerRoutine = null;
            SpikesReloaded = true;
            ListCharacters.Clear();
            ListSpikes.Clear();
            ListSpikeVictims.Clear();

            Spike[] arr = this.gameObject.GetComponentsInChildren<Spike>();
            foreach(Spike s in arr)
            {
                ListSpikes.Add(s);
            }
        }

        private void Update()
        {
            if (ListCharacters.Count != 0)
            {
                foreach(CharacterControl control in ListCharacters)
                {
                    if (!control.damageDetector.IsDead())
                    {
                        if (SpikeTriggerRoutine == null && SpikesReloaded)
                        {
                            if (!ListSpikeVictims.Contains(control))
                            {
                                ListSpikeVictims.Add(control);
                                control.damageDetector.TakeTotalDamage();
                            }
                        }
                    }
                }
            }

            foreach(CharacterControl control in ListSpikeVictims)
            {
                if (control.SkinnedMeshAnimator.avatar != null)
                {
                    if (SpikeTriggerRoutine == null && SpikesReloaded)
                    {
                        SpikeTriggerRoutine = StartCoroutine(_TriggerSpikes());
                    }
                }
            }
        }

        IEnumerator _TriggerSpikes()
        {
            SpikesReloaded = false;

            foreach(Spike s in ListSpikes)
            {
                s.Shoot();
            }

            yield return new WaitForSeconds(0.08f);

            foreach(CharacterControl control in ListSpikeVictims)
            {
                control.damageDetector.TriggerSpikeDeath(SpikeDeathAnimator);
            }

            yield return new WaitForSeconds(1.5f);

            foreach(Spike s in ListSpikes)
            {
                s.Retract();
            }

            foreach (CharacterControl control in ListSpikeVictims)
            {
                control.TurnOnRagdoll();
            }

            yield return new WaitForSeconds(1f);

            SpikeTriggerRoutine = null;
            SpikesReloaded = true;
        }

        public static bool IsTrap(GameObject obj)
        {
            if (obj.transform.root.gameObject.GetComponent<TrapSpikes>() == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            CharacterControl control = other.gameObject.transform.root.gameObject.GetComponent<CharacterControl>();

            if (control != null)
            {
                if (control.gameObject != other.gameObject)
                {
                    if (!ListCharacters.Contains(control))
                    {
                        ListCharacters.Add(control);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            CharacterControl control = other.gameObject.transform.root.gameObject.GetComponent<CharacterControl>();

            if (control != null)
            {
                if (control.gameObject != other.gameObject)
                {
                    if (ListCharacters.Contains(control))
                    {
                        ListCharacters.Remove(control);
                    }
                }
            }
        }
    }
}