using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using Sirenix.OdinInspector;
using System;
using UnityEditor;
using UnityEngine;
namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float suspicionTime = 5f;
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float waypointTolerance = 1f;
        #region DwellTime
        [SerializeField] private bool randomDwellTime;
        [HideIf("randomDwellTime")]
        [SerializeField] private float waypointDwellTime = 2.5f;
        [ShowIf("randomDwellTime")]
        [SerializeField] private float minRandomWaypointDwellTime = 1f;
        [ShowIf("randomDwellTime")]
        [SerializeField] private float maxRandomWaypointDwellTime = 5f;
        private float currentDwellTime;
        #endregion
        private GameObject player;
        private Fighter fighter;
        private Health health;
        private Mover mover;
        private ActionScheduler actionScheduler;

        private Vector3 guardPosition;
        private int currentWaypointIndex;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        private float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindGameObjectWithTag("Player");
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            guardPosition = transform.position;
            currentDwellTime = SetWaypointDwellTime();
        }
        private void Update()
        {
            if (health.IsDead()) return;
            if (InAttackRange() && fighter.CanAttack(player))
            {
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspictionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceArrivedAtWaypoint += Time.deltaTime;
            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;
            if(patrolPath != null)
            {
                if (AtWaypoint())
                {
                    currentDwellTime = SetWaypointDwellTime();
                    timeSinceArrivedAtWaypoint = 0f;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }
            if (timeSinceArrivedAtWaypoint>currentDwellTime)
            {
                mover.StartMoveAction(nextPosition);
            }
            
        }
        private float SetWaypointDwellTime()
        {
            if (randomDwellTime)
            {
                return UnityEngine.Random.Range(minRandomWaypointDwellTime, maxRandomWaypointDwellTime);
            }
            else
            {
                return waypointDwellTime;
            }
        }
        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0f;
            fighter.Attack(player);
        }

        private void SuspictionBehaviour()
        {
            actionScheduler.CancelCurrentAction();
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