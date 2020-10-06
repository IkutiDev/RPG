using Sirenix.OdinInspector;
using UnityEngine;
namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float healthPoints = 100f;
        private bool isDead=false;
        private Animator animator;
        public bool IsDead()
        {
            return isDead;
        }
        private void Start()
        {
            animator = GetComponent<Animator>();
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
        }
    }
}