using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
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

    AnimatorStateInfo animState;
    public float time;

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
        animator.SetTrigger("attack");
    }

    public bool is_playing() 
    {
        animState = animator.GetCurrentAnimatorStateInfo(0);
        time = animState.normalizedTime;
        if (time > 1.0f) { time = 0f; return false; }
        else if (time < 1.0f && time > 0f) return true;
        return false;
    }
}
