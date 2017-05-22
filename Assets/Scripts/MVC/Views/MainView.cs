using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainView : View {


    public Text testButtonText;
    public Button testButton;

	// Use this for initialization
	void Start () {

        var test = (GameModel)ApplicationFacade.instance.GetModel<GameModel>();
        testButtonText.text = test.MainViewText;

        var e = new LoadContextEvent();
        e.Context = new OtherContext();
        testButton.onClick.AddListener( delegate {
            EventSystem.instance.Dispatch(e);
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
