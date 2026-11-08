using System;
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
    public float cooldownRemain;

    public Ability(string _name, string _description, int _w, int _h, float _damageScale, float _cooldown, float _cooldownRemain = 1) 
    {
        name = _name;
        description = _description;
        w = _w;
        h = _h;
        damageScale = _damageScale;
        cooldown = _cooldown;
        cooldownRemain = _cooldownRemain;
    }
}


public class AbilityList
{
    public Ability melee_ab1 = new Ability("Forward Poke", "", 1, 4, 1.2f, 9);
    public Ability melee_ab2 = new Ability("Attack Boost", "", 0, 0, 1.2f, 10);
    public Ability melee_ab3 = new Ability("Sweet Sweep", "", 3, 1, 1.2f, 8);
    public Ability melee_ab4 = new Ability("Dash Slash", "", 3, 10, 4f, 120);

    public void apply_cooldown_reduction() 
    {
        melee_ab1.cooldownRemain = Math.Clamp(melee_ab1.cooldownRemain - Time.deltaTime, 0, 9999);
        melee_ab2.cooldownRemain = Math.Clamp(melee_ab2.cooldownRemain - Time.deltaTime, 0, 9999);
        melee_ab3.cooldownRemain = Math.Clamp(melee_ab3.cooldownRemain - Time.deltaTime, 0, 9999);
        melee_ab4.cooldownRemain = Math.Clamp(melee_ab4.cooldownRemain - Time.deltaTime, 0, 9999);
    }

    public void set_cooldown_remain(Ability ability) 
    {
        ability.cooldownRemain += ability.cooldown;
    }
}
