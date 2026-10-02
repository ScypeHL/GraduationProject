using UnityEngine;

public class AttackInput : MonoBehaviour
{
    InteractInput interactInput;
    AttackHandler attackHandler;

    private void Awake()
    {
        interactInput = GetComponent<InteractInput>();
        attackHandler = GetComponent<AttackHandler>();
    }

    public void Attack() 
    {
        attackHandler.Attack(interactInput.hoveringObjectCharacter);
    }

    public bool AttackCheck() 
    {
        return interactInput.hoveringObjectCharacter != null;
    }
}
