using UnityEngine;

public enum CellType 
{
    ARMOUR,
    HOOD,
    WEAPON
}

public class GridCellComponent : MonoBehaviour
{
    bool isOccupied = false;
    public CellType type;

    private void Start()
    {
        
    }
}
