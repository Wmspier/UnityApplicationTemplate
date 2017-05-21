using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class Test1 : MonoBehaviour {


	// Use this for initialization
	void Start () {

        EventSystem.instance.Connect<DataEvent>(Dingus);
	}


    private void Dingus(DataEvent e)
    {
        Debug.Log(e.fuff);
    }
}
