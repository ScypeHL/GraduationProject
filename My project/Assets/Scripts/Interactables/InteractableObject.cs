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
    [SerializeField] string massage;
    public InteractableObjectType type;

    private void Start()
    {
    }
    public void Interact() 
    {
        Debug.Log("This is not a portal");
        if (type == InteractableObjectType.PORTAL & massage != "")
        {
            GameSceneManager.instance.ExecuteTransition(massage);
        }
        else { }
    }
}
