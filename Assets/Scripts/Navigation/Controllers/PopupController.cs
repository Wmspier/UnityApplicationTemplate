using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : IController {

    protected GameObject View;

    public PopupController(){
        EventSystem.instance.Connect<PopupEvents.OpenPopupEvent>(OnPopupOpen);
        EventSystem.instance.Connect<PopupEvents.ClosePopupEvent>(OnPopupClose);
    }

    ~PopupController()
    {
        EventSystem.instance.Disconnect<PopupEvents.OpenPopupEvent>(OnPopupOpen);
        EventSystem.instance.Disconnect<PopupEvents.ClosePopupEvent>(OnPopupClose);
    }

    public void Update() { }

    protected virtual void InstantiateView() 
    {
        var viewRoot = GameObject.FindWithTag("PopupRoot");
        if (viewRoot == null)
            viewRoot = UnityEngine.Object.FindObjectOfType<Canvas>().gameObject;
        
        View = new GameObject("PopupView");
        View.AddComponent<Canvas>();
        View.AddComponent<PopupView>();
        View.transform.SetParent(viewRoot.gameObject.transform,false);
    }

    protected virtual void OnPopupOpen(PopupEvents.OpenPopupEvent e)
    {
        if (View == null) InstantiateView();

        View.GetComponent<PopupView>().StackPopup(e.Popup);
    }

    protected virtual void OnPopupClose(PopupEvents.ClosePopupEvent e)
    {
        if (View == null)
            return;

        View.GetComponent<PopupView>().ClosePopup();
    }

}
