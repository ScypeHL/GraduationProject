using UnityEngine;
using UnityEngine.EventSystems;

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
        if (EventSystem.current.IsPointerOverGameObject()) { return; }
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
