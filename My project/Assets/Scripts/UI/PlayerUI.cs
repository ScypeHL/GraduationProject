using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] UIPoolBar hpBar;
    [SerializeField] UIPoolBar xpBar;
    Character character;


    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Update()
    {
        hpBar.Show(character.GetStats(StatsType.Health));
        xpBar.Show(character.GetStats(StatsType.XP));
    }
}
