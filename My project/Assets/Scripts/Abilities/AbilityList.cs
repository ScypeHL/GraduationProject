using System;
using Unity.Collections;
using UnityEngine;

public enum BuffType
{ 
    ATTACK,
    MOVEMENT_SPEED,
    INVISIBILITY
}

public class Ability 
{
    public string name;
    public string description;
    public int w;
    public int h;
    public float damageScale;
    public float cooldown;
    public float cooldownRemain = 1;
    
    public int level = 1;
    public int upgradeTokenRequired;
    public float damageScaleUpgrade;
    public float cooldownReduction;

    public Ability(string _name, string _desc, int _w, int _h, float _dmg, float _dSU, float _cd, float _cdRed, int _tokenReq) 
    {
        name = _name;
        description = _desc;
        w = _w;
        h = _h;
        damageScale = _dmg;
        cooldown = _cd;

        upgradeTokenRequired = _tokenReq;
        damageScaleUpgrade = _dSU;
        cooldownReduction = _cdRed;
    }
}


public class AbilityList
{
    public Ability ab1;
    public Ability ab2;
    public Ability ab3;
    public Ability ab4;

    public Ability melee_ab1 = new Ability("Forward Poke", "Attack forward linearly", 1, 4, 2f, 0.2f, 8, 0.2f, 3);
    public Ability melee_ab2 = new Ability("Attack Boost", "Increases the attack damage for 10 seconds", 0, 0, 1.2f, 0.4f, 70, 4, 5);
    public Ability melee_ab3 = new Ability("Sweet Sweep", "Spinning strike that deals good damage to surrounding elements", 3, 1, 1.2f, 0.05f, 30, 2, 5);
    public Ability melee_ab4 = new Ability("Dash Slash", "Long range linear attak", 3, 10, 4f, 0.5f, 120, 10, 10);

    public AbilityList(string _class) 
    {
        switch (_class) 
        {
            case "Melee":
                ab1 = melee_ab1;
                ab2 = melee_ab2;
                ab3 = melee_ab3;
                ab4 = melee_ab4;
                break;
            default:
                break;
        }
    }

    public void apply_cooldown_reduction() 
    {
        ab1.cooldownRemain = Math.Clamp(ab1.cooldownRemain - Time.deltaTime, 0, 9999);
        ab2.cooldownRemain = Math.Clamp(ab2.cooldownRemain - Time.deltaTime, 0, 9999);
        ab3.cooldownRemain = Math.Clamp(ab3.cooldownRemain - Time.deltaTime, 0, 9999);
        ab4.cooldownRemain = Math.Clamp(ab4.cooldownRemain - Time.deltaTime, 0, 9999);
    }

    public Ability get_ability(int index) 
    {
        switch (index) 
        {
            case 1:
                return ab1;
            case 2:
                return ab2;
            case 3:
                return ab3;
            case 4:
                return ab4;
            default:
                return null;
        }
    }

    public void set_cooldown_remain(Ability ability) 
    {
        ability.cooldownRemain += ability.cooldown - ability.cooldownReduction * (ability.level - 1);
    }
}
