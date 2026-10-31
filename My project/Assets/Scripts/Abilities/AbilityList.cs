using System;
using UnityEngine;

public class Ability 
{
    public string name;
    public string description;
    public int w;
    public int h;
    public float damageScale;
    public float cooldown;
    public float cooldownRemain;

    public Ability(string _name, string _description, int _w, int _h, float _damageScale, float _cooldown, float _cooldownRemain = 0) 
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
    public Ability melee_ab1 = new Ability("Forward Poke", "", 1, 4, 1.2f, 2,10);
    public Ability melee_ab2 = new Ability("Forward Poke", "", 3, 1, 1.2f, 10);
    public Ability melee_ab3 = new Ability("Sweet Sweep", "", 3, 1, 1.2f, 8);
    public Ability melee_ab4 = new Ability("Dash Slash", "", 3, 10, 4f, 120);

    public void apply_cooldown_reduction() 
    {
        melee_ab1.cooldownRemain -= Time.deltaTime;
        melee_ab2.cooldownRemain -= Time.deltaTime;
        melee_ab3.cooldownRemain -= Time.deltaTime;
        melee_ab4.cooldownRemain -= Time.deltaTime;
    }

    public void set_cooldown_remain(Ability ability) 
    {
        ability.cooldownRemain += 10;
    }
}
