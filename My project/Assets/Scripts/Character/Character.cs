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
    XP,
    Level,
    UpgradePoint
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
        statsValue.Add(new Stats(StatsType.Level, 1, 100));
        statsValue.Add(new Stats(StatsType.UpgradePoint, 0));
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

    internal void Add(StatsType targetStats, int value)
    {
        statsValue[(int)targetStats].value += value;
    }

    internal void Add(StatsType targetStats, float value)
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

    internal void Set(AttributeType type, int value)
    {
        attributeValues[(int)type].value = value;
    }

    internal void Add(AttributeType type, int value)
    {
        attributeValues[(int)type].value += value;
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

    private void Update()
    {/*
        if (isDead && GetComponent<Animate>().is_playing())
        {

        }*/
    }

    public void TakeDamage(Character character, float scaling)
    {
        int totalDamage = (int)(Math.Clamp(character.GetStats(StatsType.Damage).value + character.GetAttribute(AttributeType.Strenght).value - statsGroup.statsValue[(int)StatsType.Armor].value, 1, 9999) * scaling);
        statsGroup.statsValue[(int)StatsType.Health].value -= totalDamage;
        IsDead(character);
    }

    private void IsDead(Character character) 
    {
        if (GetStats(StatsType.Health).value <= 0) 
        {
            isDead = true;
            character.AddToStats(StatsType.XP, GetStats(StatsType.XP).value);
            GetComponent<CharacterDeathHandler>().Disable();
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
        if (GetStats(StatsType.XP).value > GetStats(StatsType.XP).maxValue) 
        {
            statsGroup.statsValue[(int)StatsType.XP].value = 0;
        }
    }

    public void add_xp(int value)
    {
        statsGroup.statsValue[(int)StatsType.XP].value += value;
        if (GetStats(StatsType.XP).value > GetStats(StatsType.XP).maxValue)
        {
            statsGroup.statsValue[(int)StatsType.XP].value -= 100;
            statsGroup.statsValue[(int)StatsType.XP].maxValue += 20;
            statsGroup.statsValue[(int)StatsType.Level].value++;
            statsGroup.statsValue[(int)StatsType.UpgradePoint].value++;
        }
    }

    public Stats GetStats(StatsType type) { return statsGroup.Get(type); }
    public Attribute GetAttribute(AttributeType type) { return attributes.Get(type); }

    public void SetStats(StatsType type, int value) { statsGroup.Set(type, value); }
    public void SetAttribute(AttributeType type, int value) { attributes.Set(type, value); }

    public void AddToStats(StatsType type, int value) { statsGroup.Add(type, value); }
    public void AddToAttribute(AttributeType type, int value) { attributes.Add(type, value); }
}
