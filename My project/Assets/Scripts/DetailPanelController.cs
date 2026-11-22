using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DetailPanelController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;

    [SerializeField] GameObject statsPanel;
    [SerializeField] GameObject hud;
    [SerializeField] InventoryGrid invGrid;

    AbilityUpgradeSlot[] comps;

    [SerializeField] Character playerCharacter;
    public AbilityHitboxController hitbox;

    void Start()
    {
        int index = 0;
        AbilityUpgradeSlot[] temp1 = hud.GetComponentsInChildren<AbilityUpgradeSlot>();
        AbilityUpgradeSlot[] temp2 = statsPanel.GetComponentsInChildren<AbilityUpgradeSlot>();
        comps = new AbilityUpgradeSlot[temp1.Length + temp2.Length];
        for (int i = 0; i < temp1.Length; i++) { comps[index] = temp1[i]; index++; }
        for (int i = 0; i < temp2.Length; i++) { comps[index] = temp2[i]; index++; }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < comps.Length; i++) 
        {
            if (comps[i].isHovering)
            {
                show();
                AbilityUpgradeSlot temp = comps[i].GetComponent<AbilityUpgradeSlot>();
                title.text = temp.title;
                description.text = temp.description;
                move_panel();
                return;
            }
        }
        if (invGrid.activationTrigger) 
        {
            if (invGrid.is_there_item(Input.mousePosition)) 
            {
                show();
                InventoryItem temp = invGrid.itemMap[invGrid.selectedCell.x, invGrid.selectedCell.y];
                title.text = temp.name;
                description.text = temp.description;
                move_panel();
                return;
            }
        }
        hide();
    }

    public void move_panel() 
    {
        Vector3 temp = new Vector3(Input.mousePosition.x + 170, Input.mousePosition.y + 150, gameObject.transform.position.z);
        gameObject.transform.position = temp;
    }

    public void hide() 
    {
        gameObject.GetComponent<CanvasRenderer>().SetAlpha(0);
        description.GetComponent<CanvasRenderer>().SetAlpha(0);
        title.GetComponent<CanvasRenderer>().SetAlpha(0);
        gameObject.transform.position = new Vector3(1920, 1080, 0);
    }

    public void show()
    {
        gameObject.GetComponent<CanvasRenderer>().SetAlpha(1f);
        description.GetComponent<CanvasRenderer>().SetAlpha(1f);
        title.GetComponent<CanvasRenderer>().SetAlpha(1f);
    }

    public Character get_character() 
    {
        return playerCharacter;
    }
}
