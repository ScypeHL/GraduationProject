using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int OCCUPYING_WIDTH;
    public int OCCUPYING_HEIGHT;

    public CellType type;

    public string name;
    public string description;

    public int attack;
    public int health;
    public int defence;
    public float attack_speed;
    public float movement_speed;

    public bool isHovering;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public InventoryItem return_self_if_interacted()
    {
        if (isHovering) { return gameObject.GetComponent<InventoryItem>(); }
        else return null;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }
}
