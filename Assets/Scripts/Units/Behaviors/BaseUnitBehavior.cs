using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class BaseUnitBehavior : MonoBehaviour {

    protected Unit Owner;

    protected void Awake()
    {
        GetRequiredComponents();
        Owner = GetComponent<Unit>();
    }

    protected void GetRequiredComponents()
    {
        var type = GetType();
        foreach(var field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
        {
            var fieldType = field.FieldType;
            if (typeof(Component).IsAssignableFrom(fieldType) &&
                type.GetCustomAttributes(true).OfType<RequireComponent>().Any(c => fieldType.IsAssignableFrom(c.m_Type0) ||
                                                                fieldType.IsAssignableFrom(c.m_Type1) ||
                                                                fieldType.IsAssignableFrom(c.m_Type2))){
                field.SetValue(this, GetComponent(fieldType));
            }
        }
    }
}
