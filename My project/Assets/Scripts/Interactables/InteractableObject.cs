using UnityEngine;
using UnityEngine.TextCore.Text;

public enum InteractableObjectType 
{
    ENEMY,
    POTION,
    MONEY
}

public class InteractableObject : MonoBehaviour
{
    [SerializeField] string massage;
    public InteractableObjectType type;
    public string objectName;

    private void Start()
    {
        objectName = transform.name;
    }
    public void Interact() 
    {
        Debug.Log(massage);
        /*
        switch (target.type)
        {
            case InteractableObjectType.MONEY:
                Debug.Log("Heres your money!");
                character.charMoney = character.charMoney + 1;
                target = null;
                break;

            case InteractableObjectType.POTION:
                break;

            case InteractableObjectType.ENEMY:
                Character targetStats = target.GetComponent<Character>();
                charMovenent.Stop();

                targetStats.TakeDamage(character.GetStats(StatsType.Damage).value, character.GetAttribute(AttributeType.Strenght).value);
                animator.SetTrigger("attack");

                targetStats = null;
                target = null;
                break;
        }*/
    }
}
