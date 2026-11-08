using Mono.Cecil;
using UnityEngine;
using static Animate;

public class AttackHandler : MonoBehaviour
{
    [SerializeField] float attackRange = 4f;
    [SerializeField] float attackTime = 2f;
    float attackTimer;

    Animate animate;
    CharacterMovement charMovenent;
    Character character;

    public Character target;

    private void Awake()
    {
        animate = GetComponentInChildren<Animate>();
        charMovenent = GetComponent<CharacterMovement>();
        character = GetComponent<Character>();
    }

    internal void Attack(Character targetObject)
    {
        target = targetObject;
        ProcessAttack();
    }

    private void Update()
    {
        if (target != null && target.isDead == false)
        {
        }
        AttackTimer();
        if (target != null && character.isDead == false)
        {
            ProcessAttack();
        }
    }

    private void AttackTimer() 
    {
        if (attackTimer > 0f) { attackTimer -= Time.deltaTime; }
    }

    private void ProcessAttack() 
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < attackRange)
        {
            if (attackTimer > 0f) { target = null; return; }

            attackTimer = attackTime / character.GetStats(StatsType.AttackSpeed).fvalue;

            charMovenent.Stop();

            target.TakeDamage(character, 1);
            animate.play_attack(AttackType.AUTO_ATTACK);

            target = null;
        }
        else
        {
            charMovenent.SetDestination(target.transform.position);
        }
    }
}
