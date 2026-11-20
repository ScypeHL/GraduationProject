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

    [SerializeField] GameObject ability1Effect;

    AnimatorStateInfo animState;
    public float time;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<Character>();
        if(GetComponentInParent<PlayerInputs>() != null) ability1Effect.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (GetComponentInParent<PlayerInputs>() != null && ability1Effect.activeSelf) 
        {
            if (!is_playing(ability1Effect.GetComponent<Animator>())) { ability1Effect.SetActive(false);}
        }
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
        if (GetComponentInParent<PlayerInputs>() != null && (int)type == 1) ability1Effect.SetActive(true);
    }

    public bool is_playing(Animator _animator) 
    {
        animState = _animator.GetCurrentAnimatorStateInfo(0);
        time = animState.normalizedTime;
        if (time > 1.0f) { time = 0f; return false; }
        else if (time < 1.0f && time > 0f) return true;
        return false;
    }
}
