using Mono.Cecil;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [SerializeField] float attackRange = 4f;
    [SerializeField] float attackTime = 2f;
    float attackTimer;


    Animator animator;
    CharacterMovement charMovenent;
    Character character;

    Character target;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
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
        AttackTimer();
        if (target != null)
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
            if (attackTimer > 0f) { return; }

            attackTimer = attackTime / character.GetStats(StatsType.AttackSpeed).fvalue;

            charMovenent.Stop();

            target.TakeDamage(character.GetStats(StatsType.Damage).value, character.GetAttribute(AttributeType.Strenght).value);
            animator.SetTrigger("attack");

            target = null;
        }
        else
        {
            charMovenent.SetDestination(target.transform.position);
        }
    }
}
