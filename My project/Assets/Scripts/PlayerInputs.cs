using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] MouseInput mouseInput;
    CharacterMovementInput charMovInput;
    AttackInput attackInput;
    InteractInput interactInput;

    private void Awake()
    {
        charMovInput = GetComponent<CharacterMovementInput>();
        attackInput = GetComponent<AttackInput>();
        interactInput = GetComponent<InteractInput>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (attackInput.AttackCheck())
            {
                attackInput.Attack();
                return;
            }

            if (interactInput.InteractCheck())
            {
                interactInput.Interact();
                return;
            }

            charMovInput.MoveCharacter(); 
        }
    }
}
