using TMPro;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] UIPoolBar hpBar;
    [SerializeField] UIPoolBar xpBar;

    [SerializeField] TextMeshProUGUI ability1;
    [SerializeField] TextMeshProUGUI ability2;
    [SerializeField] TextMeshProUGUI ability3;
    [SerializeField] TextMeshProUGUI ability4;

    [SerializeField] TextMeshProUGUI maxHealth;
    [SerializeField] TextMeshProUGUI attack;
    [SerializeField] TextMeshProUGUI defence;
    [SerializeField] TextMeshProUGUI attackSpeed;
    [SerializeField] TextMeshProUGUI moveSpeed;
    [SerializeField] TextMeshProUGUI totalXP;
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI agility;
    [SerializeField] TextMeshProUGUI vitality;
    [SerializeField] TextMeshProUGUI intelligence;

    [SerializeField] TextMeshProUGUI upgradePoints;

    AbilityHitboxController hitbox;
    Character character;


    private void Awake()
    {
        character = GetComponent<Character>();
        hitbox = GetComponentInChildren<AbilityHitboxController>();
    }

    private void FixedUpdate()
    {
        AbilityList abilities = hitbox.get_abilities();
        ability1.text = ((int)(abilities.melee_ab1.cooldownRemain)).ToString();
        ability2.text = ((int)(abilities.melee_ab2.cooldownRemain)).ToString();
        ability3.text = ((int)(abilities.melee_ab3.cooldownRemain)).ToString();
        ability4.text = ((int)(abilities.melee_ab4.cooldownRemain)).ToString();

        maxHealth.text = (character.GetStats(StatsType.Health).maxValue).ToString();
        attack.text = (character.GetStats(StatsType.Damage).value).ToString();
        defence.text = (character.GetStats(StatsType.Armor).value).ToString();
        attackSpeed.text = (character.GetStats(StatsType.AttackSpeed).fvalue).ToString();
        moveSpeed.text = (character.GetStats(StatsType.MoveSpeed).fvalue).ToString();
        totalXP.text = (character.GetStats(StatsType.XP).value).ToString();
        level.text = (character.GetStats(StatsType.Level).value).ToString();
        agility.text = (character.GetAttribute(AttributeType.Strenght).value).ToString();
        vitality.text = (character.GetAttribute(AttributeType.Vitality).value).ToString();
        intelligence.text = (character.GetAttribute(AttributeType.Intelligence).value).ToString();
        upgradePoints.text = "You have " + (character.GetStats(StatsType.UpgradePoint).value).ToString() + " coins";

        hpBar.Show(character.GetStats(StatsType.Health));
        xpBar.Show(character.GetStats(StatsType.XP));
    }
}
