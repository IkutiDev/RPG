using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;
namespace RPG.Resources
{
    public class Health : MonoBehaviour,ISaveable
    {
        [SerializeField] private float healthPoints = 100f;
        private bool isDead=false;
        private Animator animator;
        private ActionScheduler actionScheduler;
        public bool IsDead()
        {
            return isDead;
        }
        private void Awake()
        {
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
        }
        private void Start()
        {
            healthPoints = GetComponent<BaseStats>().GetHealth();
        }
        public void TakeDamage(float damage)
        {
            if (isDead) return;
            healthPoints = Mathf.Max(healthPoints - damage, 0f);
            if (healthPoints == 0f)
            {
                Die();
            }
        }
        private void Die()
        {
            isDead = true;
            animator.SetTrigger("death");
            actionScheduler.CancelCurrentAction();
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            animator.Rebind();
            if (healthPoints == 0f)
            {
                Die();
            }
            else
            {
                if (isDead)
                {
                    isDead = false;
                }
            }
        }
    }
}