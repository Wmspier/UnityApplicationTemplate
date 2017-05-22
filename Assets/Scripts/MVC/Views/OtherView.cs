using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherView : View
{
    public Text testButtonText;
    public Button testButton;
    public Button backButton;

    // Use this for initialization
    void Start()
    {

        var test = (GameModel)ApplicationFacade.instance.GetModel<GameModel>();
        testButtonText.text = test.OtherViewText;

        var e = new NavigationEvents.LoadContextEvent(new MainContext(), true);
        testButton.onClick.AddListener(delegate
        {
            EventSystem.instance.Dispatch(e);
        });

        var event2 = new NavigationEvents.PreviousContextEvent();
        backButton.onClick.AddListener(delegate
        {
            EventSystem.instance.Dispatch(event2);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
