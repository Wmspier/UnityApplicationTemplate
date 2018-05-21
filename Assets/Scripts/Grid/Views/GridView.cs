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

    public CardView CardPreview;
    private Card _previewingCard;

    private GridModel _model;

    void Awake()
    {
        CardPreview.gameObject.SetActive(false);

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
        EventSystem.instance.Connect<CardEvents.CardPreviewEvent>(OnCardPreviewed);
        EventSystem.instance.Connect<CardEvents.HideCardPreviewEvent>(OnHideCardPreview);
    }

    private void OnDestroy()
    {
        EventSystem.instance.Disconnect<UnitEvents.UnitSelectedEvent>(OnUnitSelected);
        EventSystem.instance.Disconnect<CardEvents.CardPreviewEvent>(OnCardPreviewed);
        EventSystem.instance.Disconnect<CardEvents.HideCardPreviewEvent>(OnHideCardPreview);
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

    private void OnCardPreviewed(CardEvents.CardPreviewEvent e){
        CardPreview.NameText.text = e.Card.Name;
        CardPreview.DescriptionText.text = e.Card.Description;
        CardPreview.HeroismText.text = e.Card.Cost.ToString();
        CardPreview.gameObject.SetActive(true);
        _previewingCard = e.Card;
    }
    private void OnHideCardPreview(CardEvents.HideCardPreviewEvent e) {
        if (e.Card != _previewingCard)
            return;
        _previewingCard = null;
        CardPreview.gameObject.SetActive(false);
    }
}
