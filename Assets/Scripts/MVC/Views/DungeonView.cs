using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.EventSystem;
using PopupEvents;
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
            EventSystem.Instance.Dispatch(UnloadScreenEvent);
        });

        var Popup = AssetDatabase.instance.GetAsset<PopupAsset>("BASIC");
        var PopupEvent = new PopupEvents.OpenPopupEvent(new OpenPopupArgs(Popup));
        PopupButton.onClick.AddListener(delegate
        {
            EventSystem.Instance.Dispatch(PopupEvent);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
