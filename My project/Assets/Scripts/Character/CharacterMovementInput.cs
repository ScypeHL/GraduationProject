using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterMovementInput : MonoBehaviour
{
    [SerializeField] MouseInput mouseInput;
    CharacterMovement characterMovement;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
    }

    private void Update() 
    {
    }

    public void MoveCharacter() 
    {
        characterMovement.SetDestination(mouseInput.mouseInputPos);
    }
}
