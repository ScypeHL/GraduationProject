using System;
using System.Collections.Generic;
using UnityEngine;

public enum Attribute 
{
    Strenght,
    Dexterity,
    Intelligence
}

[Serializable]
public class AttributeValue
{
    public Attribute attributeType;
    public int value;

    public AttributeValue(Attribute type, int val = 0) 
    { 
        attributeType = type;
        value = val;
    }
}

[Serializable]
public class AttributeGroup
{
    public List<AttributeValue> attributeValues;
    public AttributeGroup() 
    {
        attributeValues = new List<AttributeValue>();
        attributeValues.Add(new AttributeValue(Attribute.Strenght));
        attributeValues.Add(new AttributeValue(Attribute.Dexterity));
        attributeValues.Add(new AttributeValue(Attribute.Intelligence));
    }
}

public class Character : MonoBehaviour
{
    [SerializeField] AttributeGroup attributes;

    private void Start()
    {
        attributes = new AttributeGroup();
    }
}
