using System.Data.SqlTypes;
using UnityEngine;

public class InteractInput : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI hoveringItemName;
    Character character;
    [SerializeField] TMPro.TextMeshProUGUI moneyText;
    [HideInInspector]
    public InteractableObject hoveringObject;

    private void Awake() 
    { 
        moneyText.text = 0.ToString();
        character = GetComponent<Character>();
    }
    void Update()
    {
        moneyText.text = character.charMoney.ToString();
        CheckInteractableObjects();
        if (Input.GetMouseButtonDown(0))
        {
            if (hoveringObject != null) 
            {
                hoveringObject.Interact();
            }
        }
    }

    private void CheckInteractableObjects()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            InteractableObject interactableObject = hit.transform.GetComponent<InteractableObject>();
            if (interactableObject != null)
            {
                hoveringObject = interactableObject;
                hoveringItemName.text = hoveringObject.name;
            }
            else
            {
                hoveringObject = null;
                hoveringItemName.text = "";
            }
        }
    }
}
