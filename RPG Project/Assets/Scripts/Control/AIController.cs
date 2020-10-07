using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEditor;
using UnityEngine;
namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        private GameObject player;
        private Fighter fighter;
        private Health health;
        private Mover mover;

        private Vector3 guardPosition;
        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindGameObjectWithTag("Player");
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            guardPosition = transform.position;
        }
        private void Update()
        {
            if (health.IsDead()) return;
            if (InAttackRange() && fighter.CanAttack(player)) {
                fighter.Attack(player);
            }
            else
            {
                mover.StartMoveAction(guardPosition);
            }
        }
        private bool InAttackRange()
        {
            float distanceToPlayer= Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer <= chaseDistance;
        }
        private void OnDrawGizmos()
        {
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, Vector3.down, chaseDistance);
        }
    }
}