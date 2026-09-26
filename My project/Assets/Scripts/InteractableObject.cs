using UnityEngine;

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
    }
}
