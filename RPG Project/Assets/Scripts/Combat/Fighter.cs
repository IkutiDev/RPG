using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] private float weaponRange = 2f;
        private Mover mover;
        private Transform target;
        private void Start()
        {
            mover = GetComponent<Mover>();
        }
        private void Update()
        {
            if (target == null) return;
            if (!GetIsInRange())
            {
                    mover.MoveTo(target.position);
            }
            else
            {
                mover.Stop();
            }
        }
        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
        }
        public void Cancel()
        {
            target = null;
        }
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }
    }
}