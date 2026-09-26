using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    NavMeshAgent agent;

    private void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
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
