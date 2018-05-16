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

    public View UnitInfoPopup;

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

        var event2 = new NavigationEvents.PreviousContextEvent();
        BackButton.onClick.AddListener(delegate
        {
            EventSystem.instance.Dispatch(event2);
        });

        EventSystem.instance.Connect<UnitEvents.UnitSelectedEvent>(OnUnitSelected);
    }

    private void OnUnitSelected(UnitEvents.UnitSelectedEvent e)
    {
        var popup = AssetDatabase.instance.GetAsset<PopupAsset>("UNIT_INFO");
        EventSystem.instance.Dispatch(new PopupEvents.OpenPopupAboveUnitEvent(popup, e.SelectedUnit.gameObject));
    }
}
