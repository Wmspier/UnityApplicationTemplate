using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherView : View
{
    public Text TestButtonText;
    public Button TestButton;
    public Button BackButton;
    public Button PopupButton;

    void Start()
    {

        var test = ApplicationFacade.instance.GetModel<GameModel>();
        TestButtonText.text = test.OtherViewText;

        var e = new NavigationEvents.LoadContextEvent(new MainContext(), true);
        TestButton.onClick.AddListener(delegate
        {
            EventSystem.instance.Dispatch(e);
        });

        var event2 = new NavigationEvents.PreviousContextEvent();
        BackButton.onClick.AddListener(delegate
        {
            EventSystem.instance.Dispatch(event2);
        });

        var popup = AssetDatabase.instance.GetAsset<PopupAsset>("BASIC");
        var event3 = new PopupEvents.OpenPopupEvent(popup);
        PopupButton.onClick.AddListener(delegate
        {
            EventSystem.instance.Dispatch(event3);
        });
    }
}
