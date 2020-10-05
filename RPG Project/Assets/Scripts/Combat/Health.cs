using Sirenix.OdinInspector;
using UnityEngine;
namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        // remove public in the future
        [ReadOnly] public float health = 100f;

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0f);
            Debug.Log(health);
        }
    }
}