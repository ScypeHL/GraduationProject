using UnityEngine;

public class CharacterRespawnHandler : MonoBehaviour
{
    Vector3 respawnPoint;
    string respawnSceneName;
    CharacterDeathHandler charDeath;
    [SerializeField] Animator animator;

    private void Start()
    {
        respawnPoint = transform.position;
        charDeath = GetComponent<CharacterDeathHandler>();
    }

    public void Respawn()
    {
        gameObject.transform.position = respawnPoint;
        charDeath.Enable();
        animator.Play("Standing");
    }
}
