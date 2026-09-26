using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] string massage;
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
