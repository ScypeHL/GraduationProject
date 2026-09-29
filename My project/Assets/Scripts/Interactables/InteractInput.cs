using System.Data.SqlTypes;
using UnityEngine;

public class InteractInput : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI hoverObjectTextUI;
    [SerializeField] TMPro.TextMeshProUGUI moneyText;
    [SerializeField] UIPoolBar hpBar;

    Character character;
    [HideInInspector]
    public InteractableObject hoveringObject;
    GameObject currentlyHoveringObject;
    [HideInInspector]
    public Character hoveringObjectCharacter;
    

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
