using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainView : View {


    public Button gridButton;

	// Use this for initialization
	void Start () {

        var event3 = new NavigationEvents.LoadContextEvent(new GridContext(), true);
        gridButton.onClick.AddListener(delegate {
            EventSystem.instance.Dispatch(event3);
        });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
