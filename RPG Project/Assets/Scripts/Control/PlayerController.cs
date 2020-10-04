using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Mover))]
public class PlayerController : MonoBehaviour
{
    private bool holdingMouseButtonForMovement;
    private Mover mover;
    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponent<Mover>();
    }

    // Update is called once per frame
    void Update()
    {
        if (holdingMouseButtonForMovement)
        {
            MoveToCursor();
        }
    }
    public void MovementInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            holdingMouseButtonForMovement = true;
        }
        if (context.canceled)
        {
            holdingMouseButtonForMovement = false;
        }
    }
    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if (hasHit)
        {
            mover.MoveTo(hit.point);
        }
    }
}
