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
}
