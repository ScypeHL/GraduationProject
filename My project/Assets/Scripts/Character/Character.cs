using System;
using System.Collections.Generic;
using UnityEngine;

public enum StatsType 
{ 
    Health,
    Damage,
    Armor,
    AttackSpeed
}

[Serializable]
public class Stats 
{
    public StatsType type;
    public int value;
    public int maxValue;
    public bool isFloat;
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
    public StatsGroup()
    {
        statsValue = new List<Stats>();
        statsValue.Add(new Stats(StatsType.Health, 100, 100));
        statsValue.Add(new Stats(StatsType.Damage, 10));
        statsValue.Add(new Stats(StatsType.Armor, 5));
        statsValue.Add(new Stats(StatsType.AttackSpeed, 2f));
    }

    internal Stats Get(StatsType targetStats) 
    {
        return statsValue[(int)targetStats];
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
    public int charMoney;
    [SerializeField] AttributeGroup attributes;
    public StatsGroup statsGroup;

    private void Start()
    {
        attributes = new AttributeGroup();
        statsGroup = new StatsGroup();
    }

    public void TakeDamage(int damage, int strength) 
    {
        int totalDamage = Math.Clamp(damage + strength - statsGroup.statsValue[(int)StatsType.Armor].value, 1, 9999);
        statsGroup.statsValue[(int)StatsType.Health].value -= totalDamage;
        Debug.Log("This did " + totalDamage + " damage!, Health: " + GetStats(StatsType.Health).value.ToString());
        IsDead();
    }

    private void IsDead() 
    {
        if (GetStats(StatsType.Health).value <= 0) 
        {
            Debug.Log("Enemy Dead!");
        }
    }

    public Stats GetStats(StatsType type) { return statsGroup.Get(type); }
    public Attribute GetAttribute(AttributeType type) { return attributes.Get(type); }
}
