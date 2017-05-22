using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherView : View
{
    public Text testButtonText;
    public Button testButton;

    // Use this for initialization
    void Start()
    {

        var test = (GameModel)ApplicationFacade.instance.GetModel<GameModel>();
        testButtonText.text = test.OtherViewText;

        var e = new LoadContextEvent();
        e.Context = new MainContext();
        testButton.onClick.AddListener(delegate
        {
            EventSystem.instance.Dispatch(e);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
