using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDebug : MonoBehaviour {

    public GameObject HeroPrefab;
    public GameObject UnitPrefab;
    public GameObject CardPrefab;

    private GridModel _gridModel;
    private HeroDeckModel _deckModel;

    void Awake()
    {
        EventSystem.instance.Connect<UnitEvents.PlaceHeroEvent>(OnPlaceHero);
        EventSystem.instance.Connect<UnitEvents.PlaceUnitEvent>(OnPlaceUnit);
        EventSystem.instance.Connect<DebugEvents.ResetAllRemainingMovementEvent>(OnResetAllRemainingMovement);
        _gridModel = ApplicationFacade.instance.GetModel<GridModel>();
        _deckModel = ApplicationFacade.instance.GetModel<HeroDeckModel>();
    }

    private void OnDestroy()
    {
        EventSystem.instance.Disconnect<UnitEvents.PlaceHeroEvent>(OnPlaceHero);
        EventSystem.instance.Disconnect<UnitEvents.PlaceUnitEvent>(OnPlaceUnit);
        EventSystem.instance.Disconnect<DebugEvents.ResetAllRemainingMovementEvent>(OnResetAllRemainingMovement);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            if(_deckModel != null && _deckModel.DeckContents.Count > 0)
            {
                DrawCard();
            }
        }
    }

    private void OnPlaceHero(UnitEvents.PlaceHeroEvent e)
    {
        if (_gridModel != null && _gridModel.Hero == null && _gridModel.SelectedTile != null)
        {
            var hero = Instantiate(HeroPrefab).GetComponent<Unit>();
            hero.Name = "Hero";
            _gridModel.Units.Add(hero);
            hero.OccupyingTile = _gridModel.SelectedTile;

            var heroPosition = _gridModel.SelectedTile.transform.position;
            heroPosition.y += hero.GetComponent<MeshRenderer>().bounds.extents.y;
            hero.transform.position = heroPosition;

            //_gridModel.Hero = hero.GetComponent<Hero>();
            _gridModel.SelectedTile.Selected = false;

            CreateHeroDeck();
        }
    }

    private void OnPlaceUnit(UnitEvents.PlaceUnitEvent e)
    {
        if (_gridModel != null && _gridModel.SelectedTile != null && !_gridModel.SelectedTile.Occupied)
        {
            var unit = Instantiate(UnitPrefab).GetComponent<Unit>();
            unit.Name = GridUtility.GetUniqueUnitName();
            _gridModel.Units.Add(unit);
            unit.OccupyingTile = _gridModel.SelectedTile;

            var heroPosition = _gridModel.SelectedTile.transform.position;
            heroPosition.y += unit.GetComponent<MeshRenderer>().bounds.extents.y;
            unit.transform.position = heroPosition;

            _gridModel.SelectedTile.Selected = false;
        }
    }

    private void OnResetAllRemainingMovement(DebugEvents.ResetAllRemainingMovementEvent e)
    {
        foreach(var unit in _gridModel.Units)
            unit.GetBehavior<Movement>().RemainingMovement = unit.GetBehavior<Movement>().MaxMovement;
    }

    private void CreateHeroDeck()
    {
        if (_deckModel.DeckContents != null && _deckModel.DeckContents.Count <= 0)
        {
            int deckCount = 30;

            for (int i = 0; i < deckCount; ++i)
            {
                CardModel card = new CardModel();
                card.Cost = 8;
                card.Name = "DEBUG CARD";
                card.Description = "I don't do anything";

                _deckModel.DeckContents.Add(card);
            }

            _deckModel.DeckContents.Shuffle();
        }
    }

    private void DrawCard()
    {
        var handModel = ApplicationFacade.instance.GetModel<HandModel>();

        var drawnCard = _deckModel.DrawFromDeck();
        handModel.Add(drawnCard);

        var cardObj = Instantiate(CardPrefab).GetOrAddComponent<Card>();
        cardObj.InitializeData(drawnCard);
        cardObj.InitializeView();

        var drawCardEvent = new DeckEvents.AddCardToHandEvent();
        drawCardEvent.Card = cardObj;
        EventSystem.instance.Dispatch(drawCardEvent);
    }
}
