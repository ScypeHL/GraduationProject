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
    AbilityHitboxController hitbox;
    Character character;


    private void Awake()
    {
        character = GetComponent<Character>();
        hitbox = GetComponentInChildren<AbilityHitboxController>();
    }

    private void Update()
    {
        ability1.text = "31";
        AbilityList abilities = hitbox.get_abilities();
        ability1.text = ((int)(abilities.melee_ab1.cooldownRemain)).ToString();
        ability2.text = ((int)(abilities.melee_ab2.cooldownRemain)).ToString();
        ability3.text = ((int)(abilities.melee_ab3.cooldownRemain)).ToString();
        ability4.text = ((int)(abilities.melee_ab4.cooldownRemain)).ToString();
        hpBar.Show(character.GetStats(StatsType.Health));
        xpBar.Show(character.GetStats(StatsType.XP));
    }
}
