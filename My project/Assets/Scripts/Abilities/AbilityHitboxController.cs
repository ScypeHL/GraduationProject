using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
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

    [SerializeField] GameObject markerObject;
    GameObject marker;

    private void Awake()
    {
        abilities = new AbilityList("Melee");
        marker = gameObject.transform.GetChild(0).GetChild(0).gameObject;
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

    public Ability get_ability(int id) 
    {
        switch (id)
        {
            case 1:
                return abilities.ab1;
            case 2:
                return abilities.ab2;
            case 3:
                return abilities.ab3;
            case 4:
                return abilities.ab4;
            default:
                break;
        }
        return null;
    }

    public void ExecuteAbility(int id)
    {
        if (ability.cooldownRemain <= 0)
        {
            ability = get_ability(id);
            animate.play_attack((AttackType)id);

            abilities.set_cooldown_remain(ability);
            collider.size = new Vector3(ability.w, collider.size.y, ability.h);
            collider.center = new Vector3(collider.center.x, collider.center.y, ability.h / 2);
            collider.enabled = true;
            /*if(id == 4) character.gameObject.transform.position = new Vector3(
                character.gameObject.transform.localPosition.x + 1,
                character.gameObject.transform.position.y,
                character.gameObject.transform.position.z);*/
        }
        marker.SetActive(false);
    }

    public void show_marker(int id) 
    {
        Vector3 diff = MouseInput.instance.mouseInputPos - character.transform.position;
        float angle;
        angle = Mathf.Atan2(diff.z, diff.x);
        angle = angle / Mathf.PI * 180;

        //Quaternion rotation = Quaternion.LookRotation(diff, Vector3.right);
        //markerObject.transform.localRotation = rotation;
        //hitboxMarker.transform.Rotate(0, 0, rotation.z, Space.Self);
        gameObject.transform.rotation = Quaternion.Euler(0, -angle + 90, 0);
        print(angle);

        ability = get_ability(id);
        marker.gameObject.SetActive(true);
        marker.transform.localScale = new Vector3(ability.w, ability.h, 1);
        marker.transform.localPosition = new Vector3(
            marker.transform.localPosition.x,
            ability.h / 2,
            marker.transform.localPosition.z);
    }

    public void hide_marker() 
    {
        marker.SetActive(false);
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
