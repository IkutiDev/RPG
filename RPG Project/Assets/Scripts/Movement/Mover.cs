using RPG.Core;
using UnityEngine;
using UnityEngine.AI;
namespace RPG.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] private float maximumSpeed=9f;
        private NavMeshAgent navMeshAgent;
        private Animator animator;
        private ActionScheduler actionScheduler;

        // Start is called before the first frame update
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
        }
        private void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            animator.SetFloat("forwardSpeed", speed);
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }
        public void StartMoveAction(Vector3 destination, float speedFraction) 
        {
            MoveTo(destination, speedFraction);
            actionScheduler.StartAction(this);
        }
        public void MoveTo(Vector3 destination,float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maximumSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }
    }
}