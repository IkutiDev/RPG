using UnityEngine;
using UnityEngine.InputSystem;
using RPG.Movement;
using RPG.Combat;
using RPG.Resources;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private bool movementHeld;
        private bool attackPressed;
        private Mover mover;
        private Fighter fighter;
        private Health health;
        // Start is called before the first frame update
        void Awake()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            if(health.IsDead()) { return; }
            if (InteractWithCombat()) { return; }
            if(InteractWithMovement()) { return; }
            //Debug.Log("Nothing to do");
        }

        private bool InteractWithCombat()
        {
           RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach(var hit in hits)
            {
                var target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;
                if(!fighter.CanAttack(target.gameObject)) { continue; }
                if (Mouse.current.rightButton.wasPressedThisFrame)
                {
                    fighter.Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (movementHeld)
                {
                    //full speed fraction as 2nd argument
                    mover.StartMoveAction(hit.point,1f);
                }
                return true;
            }
            return false;
        }
        public void MovementInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                movementHeld = true;
            }
            if (context.canceled)
            {
                movementHeld = false;
            }
        }

        private static Ray GetMouseRay()
        {
            //change from just mouse to possibly other ways to catch cursor
            return Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        }
    }
}