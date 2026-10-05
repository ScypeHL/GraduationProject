using System;
using UnityEngine;
using TMPro;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI moneyText;
    [SerializeField] Inventory inv;

    void Start()
    {
        
    }

    void Update()
    {
        moneyText.text = inv.money.ToString();
    }
}
