using Mono.Cecil;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [SerializeField] float attackRange = 4f;
    Animator animator;
    CharacterMovement charMovenent;
    Character character;

    InteractableObject target;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        charMovenent = GetComponent<CharacterMovement>();
        character = GetComponent<Character>();
    }

    internal void Attack(InteractableObject targetObject)
    {
        target = targetObject;
        ProcessAttack();
    }

    private void Update()
    {
        if (target != null)
        {
            ProcessAttack();
        }
    }

    private void ProcessAttack() 
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < attackRange)
        {
            switch (target.type) 
            {
                case InteractableObjectType.MONEY:
                    Debug.Log("Heres your money!");
                    character.charMoney = character.charMoney + 1;
                    target = null;
                    break;
                case InteractableObjectType.POTION:
                    break;
                case InteractableObjectType.ENEMY:
                    Character targetStats = target.GetComponent<Character>();
                    charMovenent.Stop();
                    
                    Debug.Log("Attack!");
                    targetStats.TakeDamage(character.GetStats(StatsType.Damage).value, character.GetAttribute(AttributeType.Strenght).value);
                    animator.SetTrigger("attack");
                    
                    targetStats = null;
                    target = null;
                    break;
            }
        }
        else
        {
            charMovenent.SetDestination(target.transform.position);
        }
    }
}
