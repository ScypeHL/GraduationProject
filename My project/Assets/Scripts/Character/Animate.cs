using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;

public enum AttackType
{
    AUTO_ATTACK,
    Q_ATTACK,
    W_ATTACK,
    E_ATTACK,
    R_ATTACK
}

public class Animate : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    Character character;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<Character>();
    }

    private void Update()
    {
        if (character.isDead == false)
        {
            float motion = agent.velocity.magnitude;
            animator.SetFloat("motion", motion);
        }
        else 
        {
            animator.SetBool("death", character.isDead);
        }
    }

    public void play_attack(AttackType type) 
    {
        animator.SetInteger("attackID", (int)type);
        print(animator.GetInteger("attackID"));
        animator.SetTrigger("attack");
    }

    public bool is_playing(string name) 
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(name)
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return true;
        } else return false;
    }
}
