using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

public enum InteractableObjectType 
{
    ENEMY,
    POTION,
    MONEY,
    PORTAL
}

public class InteractableObject : MonoBehaviour
{
    public event Action onTriggerEvent;

    [SerializeField] string massage;
    public InteractableObjectType type;

    private void Start()
    {
    }
    public void Interact(Inventory inv) 
    {
        onTriggerEvent?.Invoke();
        Debug.Log("This is not a portal");
        if (type == InteractableObjectType.PORTAL & massage != "")
        {
            GameSceneManager.instance.ExecuteTransition(massage);
        }
        else
        {
            switch (type)
            {
                case InteractableObjectType.MONEY:
                    PickUpObject(inv);
                    break;
            }
        }
    }

    public void PickUpObject(Inventory inv)
    {
        inv.AddMoney(5);
        Debug.Log("Picked up! Current Money: " + inv.money);
        Destroy(gameObject);
        //gameObject.SetActive(false);
    }
}
