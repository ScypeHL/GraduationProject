using UnityEngine;

public class InteractHandler : MonoBehaviour
{
    CharacterMovement charMovement;
    public InteractableObject interactedObject;
    [SerializeField] float interactionRange;
    public InteractableObject hoveringObject;
    Inventory inventory;
    Character character;

    private void Awake()
    {
        charMovement = GetComponent<CharacterMovement>();
        inventory = GetComponent<Inventory>();
        character = GetComponent<Character>();
    }

    void Start()
    {
        interactionRange = 3f;        
    }

    void Update()
    {
        if (interactedObject != null && character.isDead == false) { ProcessInteract(); }
    }

    public void ProcessInteract()
    {
        float distance = Vector3.Distance(transform.position, interactedObject.transform.position);

        if (distance < interactionRange)
        {
            interactedObject.Interact(inventory);
            charMovement.Stop();
            interactedObject = null;
        }
        else
        {
            charMovement.SetDestination(interactedObject.transform.position);
        }
    }
}
