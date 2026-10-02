using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class CharacterMovement : MonoBehaviour
{
    NavMeshAgent agent;
    Character character;
    [SerializeField] MouseInput mouseInput;

    private void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<Character>();
    }

    private void Update()
    {
        agent.speed = character.GetStats(StatsType.MoveSpeed).fvalue;

        if (Input.GetMouseButtonDown(0))
        {
            SetDestination(mouseInput.mouseInputPos);
        }
    }

    public void SetDestination(Vector3 destinationPos) 
    {
        agent.isStopped = false;
        agent.SetDestination(destinationPos);
    }

    public void Stop() 
    { 
        agent.isStopped = true;
    }
}
