using System;
using UnityEngine;
using UnityEngine.AI;

public class CharacterDeathHandler : MonoBehaviour
{
    NavMeshAgent agent;
    EnemyBehaviour enemyAI;
    Collider collider;
    
    [SerializeField] bool isPlayer = false;
    [SerializeField] GameObject deathScreen;

    AttackInput attackInput;
    InteractInput interactInput;
    CharacterMovementInput charMovInput;
    PlayerInputs playerInputs;
    Character character;

    private void Awake()
    {
        enemyAI = GetComponent<EnemyBehaviour>();
        agent = GetComponent<NavMeshAgent>();
        collider = GetComponent<Collider>();

        attackInput = GetComponent<AttackInput>();
        interactInput = GetComponent<InteractInput>();
        charMovInput = GetComponent<CharacterMovementInput>();
        playerInputs = GetComponent<PlayerInputs>();
        character = GetComponent<Character>();
    }

    public void Disable(){ SetState(false); }
    public void Enable(){ SetState(true); }

    private void SetState(bool state) 
    {
        agent.isStopped = !state;
        agent.enabled = state;

        if (enemyAI != null) { enemyAI.enabled = state; }
        collider.enabled = state;

        if (playerInputs != null)
        {
            playerInputs.enabled = state;
            charMovInput.enabled = state;
            interactInput.enabled = state;
            attackInput.enabled = state;
        }

        if (deathScreen != null) { deathScreen.SetActive(!state); }
        if (character != null & state == true) { character.Restore(); }
    }
}
