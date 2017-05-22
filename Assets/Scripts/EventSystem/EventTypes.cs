public interface BaseEvent
{
}

public struct DataEvent : BaseEvent
{

    public string fuff;
}

public struct ContextEvent : BaseEvent
{
    public bool Load;
    public Context Context;

    public ContextEvent(bool load, Context context)
    {
        Load = load;
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