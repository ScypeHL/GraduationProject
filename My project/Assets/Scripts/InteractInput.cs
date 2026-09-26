using System.Data.SqlTypes;
using UnityEngine;

public class InteractInput : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI textOnScreen;
    TMPro.TextMeshProUGUI moneyText;
    [HideInInspector]
    public InteractableObject hoveringObject;

    //private void Awake() { moneyText = ; }
    void Update()
    {
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
                textOnScreen.text = hoveringObject.name;
            }
            else
            {
                hoveringObject = null;
                textOnScreen.text = "";
            }
        }
    }
}
