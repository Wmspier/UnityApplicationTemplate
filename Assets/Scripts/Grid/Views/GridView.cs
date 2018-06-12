using UnityEngine;
using UnityEngine.UI;

public class GridView : View
{
    [Header("Buttons")]
    public Button BackButton;
    public Button GenerateGridButton;
    public Button PlaceHeroButton;
    public Button PlaceUnitButton;
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

        PlaceUnitButton.onClick.AddListener(delegate
        {
            var unitEvent = new UnitEvents.PlaceUnitEvent();
            EventSystem.instance.Dispatch(unitEvent);
        });

        ResetMovementButton.onClick.AddListener(delegate
        {
            var movementEvent = new DebugEvents.ResetAllRemainingMovementEvent();
            EventSystem.instance.Dispatch(movementEvent);
        });

        var previousContext = new NavigationEvents.PreviousContextEvent();
        BackButton.onClick.AddListener(delegate
        {
            EventSystem.instance.Dispatch(previousContext);
        });

        _model = ApplicationFacade.instance.GetModel<GridModel>();

        EventSystem.instance.Connect<UnitEvents.UnitSelectedEvent>(OnUnitSelected);
    }

    private void OnDestroy()
    {
        EventSystem.instance.Disconnect<UnitEvents.UnitSelectedEvent>(OnUnitSelected);
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
