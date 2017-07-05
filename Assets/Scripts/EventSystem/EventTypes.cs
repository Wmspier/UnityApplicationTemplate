using System;

public interface IBaseEvent{}

public class EventArgs: System.EventArgs{}

public class BaseEvent : IBaseEvent
{
    public EventArgs Args;
    protected BaseEvent(EventArgs args = null)
    {
        Args = args;
    }
}

namespace NavigationEvents
{
    public class LoadScreenEvent : BaseEvent
    {
        public LoadScreenEvent(EventArgs a) : base(a) { }
    }
    public class LoadScreenArgs : EventArgs
    {
        public string Id;
        public LoadScreenArgs(string id)
        {
            Id = id;
        }
    }
    

    public class UnloadScreen : BaseEvent { }
}

namespace ApplicationEvents
{
    public class StartUpFinishedEvent : BaseEvent{}
}

namespace PopupEvents
{
    public class OpenPopupArgs : EventArgs
    {
        public PopupAsset Popup;

        public OpenPopupArgs(PopupAsset popup)
        {
            Popup = popup;
        }
    }

    public class OpenPopupEvent : BaseEvent
    {
        public OpenPopupEvent(OpenPopupArgs args) : base(args){}
    }

    public class ClosePopupEvent : BaseEvent { }
}


