using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] MouseInput mouseInput;
    CharacterMovementInput charMovInput;
    AttackInput attackInput;
    InteractInput interactInput;
    InteractHandler interactHandler;

    private void Awake()
    {
        charMovInput = GetComponent<CharacterMovementInput>();
        attackInput = GetComponent<AttackInput>();
        interactInput = GetComponent<InteractInput>();
        interactHandler = GetComponent<InteractHandler>();
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

            interactHandler.interactedObject = null;
            charMovInput.MoveCharacter(); 
        }
    }
}
