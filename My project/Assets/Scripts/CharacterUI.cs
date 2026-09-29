using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] UIPoolBar hpBar;
    Character character;


    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Update()
    {
        hpBar.Show(character.healthPool);
    }
}
