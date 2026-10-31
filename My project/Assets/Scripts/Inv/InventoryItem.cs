using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int OCCUPYING_WIDTH;
    public int OCCUPYING_HEIGHT;

    public CellType type;

    public int attack;
    public int health;
    public int defence;
    public int attack_speed;
    public int movement_speed;

    public bool isMouseHoverinOnto;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseHoverinOnto = true;
    }

    public InventoryItem return_self_if_interacted()
    {
        if (isMouseHoverinOnto) { return gameObject.GetComponent<InventoryItem>(); }
        else return null;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseHoverinOnto = false;
    }
}
