using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : View {


    public Text testButtonText;

	// Use this for initialization
	void Start () {

        var test = (GameModel)ApplicationFacade.instance.GetModel<GameModel>();
        testButtonText.text = test.SomeData;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
