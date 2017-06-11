using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonView : View
{
    public Button BackButton;
    public Button PopupButton;

    // Use this for initialization
    void Start()
    {
        var UnloadScreenEvent = new NavigationEvents.UnloadScreen();
        BackButton.onClick.AddListener(delegate
        {
            EventSystem.instance.Dispatch(UnloadScreenEvent);
        });

        var Popup = AssetDatabase.instance.GetAsset<PopupAsset>("BASIC");
        var PopupEvent = new PopupEvents.OpenPopupEvent(Popup);
        PopupButton.onClick.AddListener(delegate
        {
            EventSystem.instance.Dispatch(PopupEvent);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
