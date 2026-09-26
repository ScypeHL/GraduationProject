using Mono.Cecil;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [SerializeField] float attackRange = 4f;
    Animator animator;
    CharacterMovement charMovenent;

    InteractableObject target;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        charMovenent = GetComponent<CharacterMovement>();
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
            charMovenent.Stop();
            Debug.Log("Attack!");
            animator.SetTrigger("attack");
            target = null;
        }
        else
        {
            charMovenent.SetDestination(target.transform.position);
        }
    }
}
