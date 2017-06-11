public interface BaseEvent{}

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

    public struct PreviousContextEvent : BaseEvent {}

    public struct LoadScreenEvent : BaseEvent {
        public string Id;
        public LoadScreenEvent(string id)
        {
            Id = id;
        }
    }

    public struct UnloadScreen : BaseEvent{}
}

namespace ApplicationEvents
{
    public struct StartUpFinishedEvent : BaseEvent{}
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

    public struct ClosePopupEvent : BaseEvent{}
}


