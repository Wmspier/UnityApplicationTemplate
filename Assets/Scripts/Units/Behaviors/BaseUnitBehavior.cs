using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class BaseUnitBehavior : MonoBehaviour {

    protected Unit Owner;

    protected void Awake()
    {
        Owner = GetComponent<Unit>();
    }
}
