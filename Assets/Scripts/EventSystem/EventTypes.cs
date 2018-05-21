using UnityEngine;

public interface BaseEvent
{
}

namespace NavigationEvents
{ 
    public struct LoadContextEvent : BaseEvent {
        public Context Context;
        public bool Back;

        public LoadContextEvent(Context context, bool back)
        {
            Context = context;
            Back = back;
        }
    }

    public struct PreviousContextEvent : BaseEvent { 
    }
}

namespace DataEvents
{ 
    public struct SetTextEvent : BaseEvent {
        public string Text;
        public SetTextEvent(string text)
        {
            Text = text;
        }
    }
}

namespace PopupEvents
{
    public struct OpenPopupEvent : BaseEvent{
        public PopupAsset Popup;
        public OpenPopupEvent(PopupAsset popup)
        {
            Popup = popup;
        }
    }

    public struct OpenPopupAboveUnitEvent : BaseEvent{

        public PopupAsset Popup;
        public GameObject Unit;
        public OpenPopupAboveUnitEvent(PopupAsset popup, GameObject o)
        {
            Popup = popup;
            Unit = o;
        }
    }

    public struct ClosePopupEvent : BaseEvent {}
}

namespace GridEvents
{
    public struct CreateDataEvent : BaseEvent {}

    public struct ConstructGridEvent : BaseEvent{}

    public struct GridConstructedEvent : BaseEvent{
        public GridConstructedEvent(int rows, int columns, float size, Vector3 center)
        {
            Rows = rows;
            Columns = columns;
            TileSize = size;
            GridCenter = center;
        }
        public int Rows;
        public int Columns;
        public float TileSize;
        public Vector3 GridCenter;
    }

    public struct TileSelectedEvent : BaseEvent {
        public TileSelectedEvent(Tile selectedTile)
        {
            SelectedTile = selectedTile;
        }
        public Tile SelectedTile;
    }
}

namespace UnitEvents
{
    public struct PlaceHeroEvent : BaseEvent
    {
    }
    public struct PlaceUnitEvent : BaseEvent
    {
    }
    public struct UnitSelectedEvent : BaseEvent
    {
        public UnitSelectedEvent(Unit unit)
        {
            SelectedUnit = unit;
        }
        public Unit SelectedUnit;
    }
    public struct UnitMoveEvent : BaseEvent {
        public UnitMoveEvent(Unit unit)
        {
            SelectedUnit = unit;
        }
        public Unit SelectedUnit;
    }
    public struct UnitKilledEvent : BaseEvent {
        public UnitKilledEvent(Unit unit)
        {
            Unit = unit;
        }
        public Unit Unit;
    }
    public struct UnitStateChangeEvent : BaseEvent {
        public UnitStateChangeEvent(Unit.UnitState state, Unit unit)
        {
            State = state;
            Unit = unit;
        }
        public Unit.UnitState State;
        public Unit Unit;
    }
}

namespace CardEvents {
    public struct CardPreviewEvent : BaseEvent {
        public CardPreviewEvent(Card card) {
            Card = card;
        }
        public Card Card;
    }
    public struct HideCardPreviewEvent : BaseEvent {
        public HideCardPreviewEvent(Card card)
        {
            Card = card;
        }
        public Card Card;
    }
}

namespace DebugEvents
{
    public struct ResetAllRemainingMovementEvent : BaseEvent {
    }
}
