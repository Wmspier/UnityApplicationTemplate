using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridView : View
{
    [Header("Buttons")]
    public Button BackButton;
    public Button GenerateGridButton;
    public Button PlaceHeroButton;

    void Start()
    {

        GenerateGridButton.onClick.AddListener(delegate
        {
            var gridEvent = new GridEvents.CreateDataEvent();
            EventSystem.instance.Dispatch(gridEvent);
        });

        var event2 = new NavigationEvents.PreviousContextEvent();
        BackButton.onClick.AddListener(delegate
        {
            EventSystem.instance.Dispatch(event2);
        });
    }
}
