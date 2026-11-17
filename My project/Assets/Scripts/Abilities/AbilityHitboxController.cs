using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using static AbilityList;
using static Animate;

public class AbilityHitboxController : MonoBehaviour
{
    BoxCollider collider;
    
    Character enemycharacter;
    Character character;
    Animate animate;

    public AbilityList abilities;
    Ability ability;

    private void Awake()
    {
        abilities = new AbilityList("Melee");
    }

    void Start()
    {
        animate = GetComponentInParent<Animate>();
        character = GetComponentInParent<Character>();
        
        collider = GetComponent<BoxCollider>();
        collider.size = new Vector3(1f, 2f, 1f);
        collider.center = new Vector3(-0.15f, 0f, 1f);
        collider.enabled = false;
    }

    private void Update() 
    {
        abilities.apply_cooldown_reduction();
    }

    public AbilityList get_abilities() 
    {
        return abilities;
    }

    public void ExecuteAbility(int id)
    {
        switch (id)
        {
            case 1:
                ability = abilities.ab1;
                break;
            case 2:
                ability = abilities.ab2;
                break;
            case 3:
                ability = abilities.ab3;
                break;
            case 4:
                ability = abilities.ab4;
                break;
        }
        if (ability.cooldownRemain <= 0)
        {
            animate.play_attack((AttackType)id);

            abilities.set_cooldown_remain(ability);
            collider.size = new Vector3(ability.w, collider.size.y, ability.h);
            collider.center = new Vector3(collider.center.x, collider.size.y, ability.h / 2);
            collider.enabled = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        enemycharacter = collision.gameObject.GetComponent<Character>();
        if (collision.gameObject.GetComponent<PlayerInputs>() == null) 
        { 
            enemycharacter.TakeDamage(character, ability.damageScale + ability.damageScaleUpgrade * (ability.level - 1));
        }
        collider.enabled = false;
    }

    void OnCollisionExit(Collision collision)
    {
        enemycharacter = null;
        collider.enabled = false;
    }
}
