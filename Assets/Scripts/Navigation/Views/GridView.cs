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
    public Button ResetMovementButton;

    private GridModel _model;

    void Awake()
    {
        GenerateGridButton.onClick.AddListener(delegate
        {
            var gridEvent = new GridEvents.CreateDataEvent();
            EventSystem.instance.Dispatch(gridEvent);
        });

        PlaceHeroButton.onClick.AddListener(delegate
        {
            var heroEvent = new UnitEvents.PlaceHeroEvent();
            EventSystem.instance.Dispatch(heroEvent);
        });

        ResetMovementButton.onClick.AddListener(delegate
        {
            var movementEvent = new DebugEvents.ResetAllRemainingMovementEvent();
            EventSystem.instance.Dispatch(movementEvent);
        });

        var event2 = new NavigationEvents.PreviousContextEvent();
        BackButton.onClick.AddListener(delegate
        {
            EventSystem.instance.Dispatch(event2);
        });

        _model = ApplicationFacade.instance.GetModel<GridModel>();

        EventSystem.instance.Connect<UnitEvents.UnitSelectedEvent>(OnUnitSelected);
    }

    private void OnUnitSelected(UnitEvents.UnitSelectedEvent e)
    {
        if (_model.SelectedUnit != null)
            return;
        else
            _model.SelectedUnit = e.SelectedUnit; 

        var popup = AssetDatabase.instance.GetAsset<PopupAsset>("UNIT_INFO");
        EventSystem.instance.Dispatch(new PopupEvents.OpenPopupEvent(popup));
    }
}
