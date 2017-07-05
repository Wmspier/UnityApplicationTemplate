using System;
using System.Collections.Generic;
using Assets.Scripts.EventSystem;
using PopupEvents;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : Controller {

    static public GameObject View;

    public PopupController(){
        EventSystem.Instance.Connect<PopupEvents.OpenPopupEvent>(OnPopupOpen);
        EventSystem.Instance.Connect<PopupEvents.ClosePopupEvent>(OnPopupClose);
    }

    private void InstantiateView() {

        var viewRoot = UnityEngine.Object.FindObjectOfType<Canvas>();

        View = new GameObject("PopupView");
        View.AddComponent<Canvas>();
        View.AddComponent<PopupView>();
        View.transform.SetParent(viewRoot.gameObject.transform,false);
    }

    private void OnPopupOpen(PopupEvents.OpenPopupEvent e)
    {
        if (View == null) InstantiateView();

        var args = (OpenPopupArgs)e.Args;
        View.GetComponent<PopupView>().StackPopup(args.Popup);
    }

    private void OnPopupClose(PopupEvents.ClosePopupEvent e)
    {
        if (View == null) InstantiateView();

        View.GetComponent<PopupView>().ClosePopup();
    }

}
