using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainView : View {


    public Text testButtonText;
    public Button testButton;
    public Button gridButton;
    public Button backButton;

	// Use this for initialization
	void Start () {

        var test = (GameModel)ApplicationFacade.instance.GetModel<GameModel>();
        testButtonText.text = test.MainViewText;

        var event1 = new NavigationEvents.LoadContextEvent(new OtherContext(), true);
        testButton.onClick.AddListener( delegate {
            EventSystem.instance.Dispatch(event1);
        });        

        var event2 = new NavigationEvents.PreviousContextEvent();
        backButton.onClick.AddListener(delegate
        {
            EventSystem.instance.Dispatch(event2);
        });

        var event3 = new NavigationEvents.LoadContextEvent(new GridContext(), true);
        gridButton.onClick.AddListener(delegate {
            EventSystem.instance.Dispatch(event3);
        });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
