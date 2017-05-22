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
