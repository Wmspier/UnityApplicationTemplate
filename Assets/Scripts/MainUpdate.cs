using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.EventSystem;
using UnityEngine;

public class MainUpdate : MonoBehaviour {
	void Update ()
	{
	    EventSystem.Instance.Update(Time.deltaTime);
	}
}
