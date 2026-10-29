using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] MouseInput mouseInput;
    CharacterMovementInput charMovInput;
    AttackInput attackInput;
    InteractInput interactInput;
    InteractHandler interactHandler;
    AbilityHitboxController hitbox;
    public static PlayerInputs instance;
    public event Action onRightClick;

    private void Awake()
    {
        instance = this;
        charMovInput = GetComponent<CharacterMovementInput>();
        attackInput = GetComponent<AttackInput>();
        interactInput = GetComponent<InteractInput>();
        interactHandler = GetComponent<InteractHandler>();
        hitbox = GetComponentInChildren<AbilityHitboxController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            onRightClick?.Invoke();
        }
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
        if (Input.GetKeyDown(KeyCode.Q)) { hitbox.ExecuteAbility(1); }
        if (Input.GetKeyDown(KeyCode.W)) { }
        if (Input.GetKeyDown(KeyCode.E)) { hitbox.ExecuteAbility(3); }
        if (Input.GetKeyDown(KeyCode.R)) { hitbox.ExecuteAbility(4); }

    }
}
