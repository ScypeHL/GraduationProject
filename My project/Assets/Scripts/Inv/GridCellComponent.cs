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

    private void Update()
    {

    }

    public void apply_item_stats() 
    {
        Character character;
        character = PlayerInputs.instance.gameObject.GetComponent<Character>();
        character.statsGroup.Add(StatsType.Damage, item.attack);
        character.statsGroup.Add(StatsType.AttackSpeed, item.attack_speed);
        character.statsGroup.Add(StatsType.Armor, item.defence);
        character.statsGroup.statsValue[(int)(StatsType.Health)].maxValue += item.health;
        character.statsGroup.Add(StatsType.Health, item.health);
        character.statsGroup.Add(StatsType.MoveSpeed, item.movement_speed);
    }

    public void remove_item_stats()
    {
        Character character;
        character = PlayerInputs.instance.gameObject.GetComponent<Character>();
        character.statsGroup.Remove(StatsType.Damage, item.attack);
        character.statsGroup.Remove(StatsType.AttackSpeed, item.attack_speed);
        character.statsGroup.Remove(StatsType.Armor, item.defence);
        character.statsGroup.statsValue[(int)(StatsType.Health)].maxValue -= item.health;
        character.statsGroup.Remove(StatsType.Health, item.health);
        character.statsGroup.Remove(StatsType.MoveSpeed, item.movement_speed);
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
