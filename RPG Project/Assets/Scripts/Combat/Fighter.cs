using RPG.Core;
using RPG.Movement;
using UnityEngine;
namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        //should be in weapon config
        
        [SerializeField] private float timeBetweenAttacks;
        
        [SerializeField] private Transform handTransform = null;
        [SerializeField] private Weapon weapon = null;

        private Mover mover;
        private Health target;
        private ActionScheduler actionScheduler;
        private Animator animator;
        private float timeSinceLastAttack = Mathf.Infinity;
        private void Awake()
        {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
        }
        private void Start()
        {
            SpawnWeapon();
        }
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead()) return;
            if (!GetIsInRange())
            {
                //full speed fraction as 2nd argument
                mover.MoveTo(target.transform.position,1f);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour(); 
            }
        }
        private void SpawnWeapon()
        {
            if (weapon == null) return;
            weapon.Spawn(handTransform, animator);
        }
        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                //This will trigger Animation Event Hit()
                TriggerAttack();
                timeSinceLastAttack = 0f;
            }
        }

        private void TriggerAttack()
        {
            animator.ResetTrigger("stopAttack");
            animator.SetTrigger("attack");
        }

        //Animation Event
        void Hit()
        {
            if (target == null) return;
            if (weapon == null) return;
            target.TakeDamage(weapon.GetDamage());
        }
        public void Attack(GameObject combatTarget)
        {
            actionScheduler.StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }
        public void Cancel()
        {
            StopAttack();
            target = null;
            mover.Cancel();
        }

        private void StopAttack()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }
        private bool GetIsInRange()
        {
            if (weapon == null) return false;
            return Vector3.Distance(transform.position, target.transform.position) < weapon.GetRange();
        }

    }
}