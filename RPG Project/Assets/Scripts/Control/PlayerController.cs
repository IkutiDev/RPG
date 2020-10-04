using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using RPG.Movement;
using RPG.Combat;
using System;

namespace RPG.Control
{
    [RequireComponent(typeof(Mover))][RequireComponent(typeof(Fighter))]
    public class PlayerController : MonoBehaviour
    {
        private bool movementHeld;
        private bool attackPressed;
        private Mover mover;
        private Fighter fighter;
        // Start is called before the first frame update
        void Start()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
        }

        // Update is called once per frame
        void Update()
        {
            if (InteractWithCombat()) { return; }
            if(InteractWithMovement()) { return; }
            Debug.Log("Nothing to do");
        }

        private bool InteractWithCombat()
        {
           RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach(var hit in hits)
            {
                var target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) { continue; }
                if (Mouse.current.rightButton.wasPressedThisFrame)
                {
                    fighter.Attack(target);
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
                    mover.MoveTo(hit.point);
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
        //public void AttackInput(InputAction.CallbackContext context)
        //{
        //    if (context.performed)
        //    {
        //        attackPressed = true;
        //    }
        //}

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        }
    }
}