using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int money;

    public void AddMoney(int amount)
    {
        money += amount;
    }
}
