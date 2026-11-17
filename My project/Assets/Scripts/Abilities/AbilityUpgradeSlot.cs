using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public enum UpgradeType 
{ 
    BASIC,
    ADVANCED,
}

public class AbilityUpgradeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string title;
    public string description;
    public bool isHovering;
    public bool isUpgraded;
    [SerializeField] TextMeshProUGUI underlineText;
    [SerializeField] DetailPanelController detailPanel;
    Ability ability;
    [SerializeField] int id;
    public UpgradeType type;

    private void Start() 
    {
        ability = detailPanel.hitbox.abilities.get_ability(id);
        if (underlineText != null && ability == null) underlineText.GetComponent<CanvasRenderer>().SetAlpha(0f);
        if (ability != null) { title = ability.name; description = ability.description; underlineText.text = "Level 1"; }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

    public void upgrade() 
    {
        if (isUpgraded == true) ;
        Character temp = detailPanel.get_character();
        if (temp.GetStats(StatsType.UpgradePoint).value > 0)
        {
            if (type == UpgradeType.BASIC)
            {
                ability.level++;
                underlineText.text = "Level " + ability.level;
            }
            else if (type == UpgradeType.ADVANCED)
            {
                underlineText.text = "owned";
            }
            temp.GetStats(StatsType.UpgradePoint).value--;
            isUpgraded = true;
            underlineText.GetComponent<CanvasRenderer>().SetAlpha(1f);
        }
    }
}
