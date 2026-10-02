using System.Data.SqlTypes;
using UnityEngine;

public class InteractInput : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI hoverObjectTextUI;
    [SerializeField] TMPro.TextMeshProUGUI moneyText;
    [SerializeField] UIPoolBar hpBar;

    Character character;
    CharacterMovement charMovement;
    
    [HideInInspector]
    public InteractableObject hoveringObject;
    GameObject currentlyHoveringObject;
    
    [HideInInspector]
    public Character hoveringObjectCharacter;
    InteractableObject interactedObject;
    [SerializeField] float interactionRange;



    private void Awake() 
    {
        interactionRange = 3f;
        moneyText.text = 0.ToString();
        character = GetComponent<Character>();
        charMovement = GetComponent<CharacterMovement>();
    }
    void Update()
    {
        moneyText.text = character.charMoney.ToString();
        CheckInteractableObjects();
        if (interactedObject != null) { ProcessInteract(); }
    }

    public void Interact() { interactedObject = hoveringObject; }

    public void ProcessInteract()
    {
        float distance = Vector3.Distance(transform.position, interactedObject.transform.position);

        if(distance < interactionRange)
        {
            interactedObject.Interact();
            charMovement.Stop();
            interactedObject = null;
        }
        else
        {
            charMovement.SetDestination(interactedObject.transform.position);
        }
    }

    public bool InteractCheck()
    {
        return hoveringObject != null;
    }

    private void CheckInteractableObjects()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (currentlyHoveringObject != hit.transform.gameObject)
            {
                InteractableObject interactableObject = hit.transform.GetComponent<InteractableObject>();
                if (interactableObject != null)
                {
                    hoveringObject = interactableObject;
                    hoveringObjectCharacter = interactableObject.GetComponent<Character>();
                    hoverObjectTextUI.text = hoveringObject.name;
                }
                else
                {
                    hoveringObjectCharacter = null;
                    hoveringObject = null;
                    hoverObjectTextUI.text = "";
                }
            }
            else 
            { 
            
            }
            HPBarUpdate();
        }
    }

    private void HPBarUpdate()
    {
        if (hoveringObjectCharacter != null)
        {
            hpBar.Show(hoveringObjectCharacter.GetStats(StatsType.Health));
        }
        else 
        {
            hpBar.Clear();
        }
    }
}
