using System;
using System.Collections.Generic;
using UnityEngine;

public enum StatsType 
{ 
    Health,
    Damage,
    Armor
}

[Serializable]
public struct Stats 
{
    public StatsType type;
    public int value;
    public int maxValue;
    public Stats(StatsType _type, int _value = 0, int _maxValue = 9999) 
    { 
        value = _value;
        type = _type;
        maxValue = _maxValue;
    }
}

public enum AttributeType
{
    Strenght,
    Vitality,
    Intelligence
}

[Serializable]
public struct AttributeStruct
{
    public AttributeType type;
    public int value;
    public AttributeStruct(AttributeType _type, int _value = 0) 
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
    }

    internal Stats Get(StatsType targetStats) 
    {
        return statsValue[(int)targetStats];
    }
}


[Serializable]
public class AttributeGroup
{
    public List<AttributeStruct> attributeValues;
    public AttributeGroup() 
    {
        attributeValues = new List<AttributeStruct>();
        attributeValues.Add(new AttributeStruct(AttributeType.Strenght));
        attributeValues.Add(new AttributeStruct(AttributeType.Vitality));
        attributeValues.Add(new AttributeStruct(AttributeType.Intelligence));
    }
    internal AttributeStruct Get(AttributeType type)
    {
        return attributeValues[(int)type];
    }
}

public class Character : MonoBehaviour
{
    public int charMoney;
    [SerializeField] AttributeGroup attributes;
    [SerializeField] StatsGroup statsGroup;

    private void Start()
    {
        attributes = new AttributeGroup();
        statsGroup = new StatsGroup();
    }

    public void TakeDamage(int damage, int strength) 
    {
        int totalDamage = Math.Clamp(damage + strength - statsGroup.statsValue[(int)StatsType.Armor].value, 1, 9999);
        int health = statsGroup.statsValue[(int)StatsType.Health].value;
        health -= totalDamage;
        statsGroup.statsValue[0] = new Stats(StatsType.Health, health);
        Debug.Log("This did " + totalDamage + " damage!, Health: " + health);
        IsDead();
    }

    private void IsDead() 
    {
        if (statsGroup.statsValue[0].value <= 0) 
        {
            Debug.Log("Enemy Dead!");
        }
    }

    public Stats GetStats(StatsType type) { return statsGroup.Get(type); }
    public AttributeStruct GetAttribute(AttributeType type) { return attributes.Get(type); }
}
