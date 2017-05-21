using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void DotIt () {

        DataEvent e = new DataEvent();
        e.fuff = "fuff";
        EventSystem.instance.Dispatch(e);
	}
}
