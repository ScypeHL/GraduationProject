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

    private bool abilityTrigger = false;

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
            abilityTrigger = false;
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
            MouseInput.instance.moveMarker.SetActive(false);
            MouseInput.instance.moveMarker.SetActive(true);
            MouseInput.instance.moveMarker.transform.position = MouseInput.instance.mouseInputPos;
        }

        if (Input.GetKey(KeyCode.Q)) { hitbox.show_marker(1); abilityTrigger = true; }
        if (Input.GetKey(KeyCode.W)) //{ hitbox.show_marker(1); }
        if (Input.GetKey(KeyCode.E)) { hitbox.show_marker(3); abilityTrigger = true; }
        if (Input.GetKey(KeyCode.R)) { hitbox.show_marker(4); abilityTrigger = true; }

        if (abilityTrigger == true)
        {
            if (Input.GetKeyUp(KeyCode.Q)) { hitbox.ExecuteAbility(1); }
            if (Input.GetKeyUp(KeyCode.W)) { hitbox.ExecuteAbility(2); }
            if (Input.GetKeyUp(KeyCode.E)) { hitbox.ExecuteAbility(3); print("hellow"); }
            if (Input.GetKeyUp(KeyCode.R)) { hitbox.ExecuteAbility(4); }
        }
    }
}
