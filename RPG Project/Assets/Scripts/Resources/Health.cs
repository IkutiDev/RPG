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
        private BaseStats baseStats;
        public bool IsDead()
        {
            return isDead;
        }
        private void Awake()
        {
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
            baseStats = GetComponent<BaseStats>();
        }
        private void Start()
        {
            healthPoints = baseStats.GetHealth();
        }
        public void TakeDamage(GameObject insigator,float damage)
        {
            if (isDead) return;
            healthPoints = Mathf.Max(healthPoints - damage, 0f);
            if (healthPoints == 0f)
            {
                AwardExperience(insigator);
                Die();
            }
        }
        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;
            experience.GainExperience(baseStats.GetExperience());
        }
        public float GetPercentage()
        {
            return healthPoints / baseStats.GetHealth() * 100f;
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