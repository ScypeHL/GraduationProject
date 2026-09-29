using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] MouseInput mouseInput;

    private void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
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
