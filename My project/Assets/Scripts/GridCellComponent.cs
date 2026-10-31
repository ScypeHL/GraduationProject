using UnityEngine;
using UnityEngine.EventSystems;

public enum CellType 
{
    ARMOUR,
    HOOD,
    WEAPON
}

public class GridCellComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isOccupied = false;
    bool isMouseHoverinOnto = false;
    public CellType type;
    public InventoryItem item;
    InventoryGrid grid;
    GridCellComponent self;

    private void Start()
    {
        grid = FindAnyObjectByType<InventoryGrid>();
        self = GetComponent<GridCellComponent>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseHoverinOnto = true;
    }

    public bool interact() 
    {
        return isMouseHoverinOnto;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseHoverinOnto = false;
    }
}
