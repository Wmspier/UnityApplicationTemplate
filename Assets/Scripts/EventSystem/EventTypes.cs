public interface BaseEvent
{
}

public struct DataEvent : BaseEvent
{
    public string fuff;
}

public struct LoadContextEvent : BaseEvent
{
    public Context Context;

    public LoadContextEvent(Context context)
    {
        Context = context;
    }
}

public struct SetTextEvent : BaseEvent
{
    public string Text;
    public SetTextEvent(string text)
    {
        Text = text;
    }
}