using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;
using System.Linq.Expressions;

public class AbilityHitboxController : MonoBehaviour
{
    BoxCollider collider;
    
    Character enemycharacter;
    Character character;

    AbilityList abilities;
    AbilityList.Ability ability;

    void Start()
    {
        abilities = new AbilityList();
        character = GetComponentInParent<Character>();
        
        collider = GetComponent<BoxCollider>();
        collider.size = new Vector3(1f, 2f, 1f);
        collider.center = new Vector3(-0.15f, 0f, 1f);
        collider.enabled = false;
    }

    public void ExecuteAbility(int id)
    {
        Debug.Log("Resize!");

        switch (id)
        {
            case 1:
                ability = abilities.melee_ab1;
                break;
            case 2:
                ability = abilities.melee_ab2;
                break;
            case 3:
                ability = abilities.melee_ab3;
                break;
            case 4:
                ability = abilities.melee_ab4;
                break;
        }

        collider.size = new Vector3(ability.w, collider.size.y, ability.h);
        collider.center = new Vector3(collider.center.x, collider.size.y, ability.h / 2);
        collider.enabled = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(ability.name);
        enemycharacter = collision.gameObject.GetComponent<Character>();
        if (collision.gameObject.GetComponent<PlayerInputs>() == null) 
        { 
            enemycharacter.TakeDamage(
                (int)(character.GetStats(StatsType.Damage).value * ability.damageScale), 
                character.GetAttribute(AttributeType.Strenght).value); 
        }
        collider.enabled = false;
    }
}
