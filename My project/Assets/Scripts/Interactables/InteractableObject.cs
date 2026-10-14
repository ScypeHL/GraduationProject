using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

public enum InteractableObjectType 
{
    ENEMY,
    POTION,
    MONEY,
    PORTAL,
    NPC
}

public class InteractableObject : MonoBehaviour
{

    [SerializeField] string massage;
    public InteractableObjectType type;

    private void Start()
    {
    }

    public void Interact(Inventory inv) 
    {
        switch (type)
        {
            case InteractableObjectType.PORTAL:
                if (massage != "") { GameSceneManager.instance.ExecuteTransition(massage); }
                break;
            case InteractableObjectType.MONEY:
                PickUpObject(inv);
                break;
            case InteractableObjectType.NPC:
                Debug.Log("here");
                break;
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
