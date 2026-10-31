using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public enum StatsType 
{ 
    Health,
    Damage,
    Armor,
    AttackSpeed,
    MoveSpeed,
    XP
}

[Serializable]
public class Stats 
{
    public StatsType type;
    public int value;
    public int maxValue;
    public float fvalue;
    public Stats(StatsType _type, int _value = 0, int _maxValue = 9999)
    { 
        value = _value;
        type = _type;
        maxValue = _maxValue;
    }

    public Stats(StatsType _type, float _fvalue = 0)
    {
        fvalue = _fvalue;
        type = _type;
    }
}

public enum AttributeType
{
    Strenght,
    Vitality,
    Intelligence
}

[Serializable]
public class Attribute
{
    public AttributeType type;
    public int value;
    public Attribute(AttributeType _type, int _value = 0) 
    { 
        value = _value;
        type = _type;
    }
}

[Serializable]
public class StatsGroup 
{
    public List<Stats> statsValue;
    public StatsGroup(int health, int damage, int armor, float attackSpeed, float moveSpeed, int xp)
    {
        statsValue = new List<Stats>();
        statsValue.Add(new Stats(StatsType.Health, health, health));
        statsValue.Add(new Stats(StatsType.Damage, damage));
        statsValue.Add(new Stats(StatsType.Armor, armor));
        statsValue.Add(new Stats(StatsType.AttackSpeed, attackSpeed));
        statsValue.Add(new Stats(StatsType.MoveSpeed, moveSpeed));
        statsValue.Add(new Stats(StatsType.XP, xp, 100));
    }

    internal Stats Get(StatsType targetStats) 
    {
        return statsValue[(int)targetStats];
    }

    internal void Set(StatsType targetStats, int value) 
    {
         statsValue[(int)targetStats].value = value;
    }

    internal void Set(StatsType targetStats, float value)
    {
        statsValue[(int)targetStats].fvalue = value;
    }
}


[Serializable]
public class AttributeGroup
{
    public List<Attribute> attributeValues;
    public AttributeGroup() 
    {
        attributeValues = new List<Attribute>();
        attributeValues.Add(new Attribute(AttributeType.Strenght));
        attributeValues.Add(new Attribute(AttributeType.Vitality));
        attributeValues.Add(new Attribute(AttributeType.Intelligence));
    }
    internal Attribute Get(AttributeType type)
    {
        return attributeValues[(int)type];
    }
}

public class Character : MonoBehaviour
{
    [SerializeField] AttributeGroup attributes;
    [HideInInspector] public StatsGroup statsGroup;
    public bool isDead;

    public int Health;
    public int Damage;
    public int Armor = 0;
    public float AttackSpeed;
    public float MoveSpeed;
    public int XP;


    private void Start()
    {
        attributes = new AttributeGroup();
        statsGroup = new StatsGroup(Health, Damage, Armor, AttackSpeed, MoveSpeed, XP);
    }

    public void TakeDamage(int damage, int strength) 
    {
        int totalDamage = Math.Clamp(damage + strength - statsGroup.statsValue[(int)StatsType.Armor].value, 1, 9999);
        statsGroup.statsValue[(int)StatsType.Health].value -= totalDamage;
        IsDead();
    }

    private void IsDead() 
    {
        if (GetStats(StatsType.Health).value <= 0) 
        {
            isDead = true;
            GetComponent<CharacterDeathHandler>().Disable();
            while (GetComponent<Animate>().is_playing("Death")) print("waiting for death\n");
            if (GetComponent<PlayerInputs>() == null) Destroy(gameObject);
        }
    }

    internal void Restore()
    {
        statsGroup.statsValue[(int)StatsType.Health].value = GetStats(StatsType.Health).maxValue;
        isDead = false;
    }

    public void add_xp(Character target)
    {
        statsGroup.statsValue[(int)StatsType.XP].value += target.statsGroup.statsValue[(int)StatsType.XP].value;
        print(GetStats(StatsType.XP).value);
        if (GetStats(StatsType.XP).value > GetStats(StatsType.XP).maxValue) 
        {
            statsGroup.statsValue[(int)StatsType.XP].value = 0;
        }
    }

    public Stats GetStats(StatsType type) { return statsGroup.Get(type); }
    public Attribute GetAttribute(AttributeType type) { return attributes.Get(type); }
}
