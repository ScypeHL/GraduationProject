using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{

    AttackHandler attackHandler;
    [SerializeField] Character target;
    float timer = 4f;

    private void Awake()
    {
        attackHandler = GetComponent<AttackHandler>();
    }

    private void Start()
    {
        target = GameManager.instance.player.GetComponent<Character>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f) { timer = 4f; attackHandler.Attack(target); }
    }
}
