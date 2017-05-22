/// <summary>
/// Contexts are references to two scenes (World and View).
/// 
/// They are intended to be loaded by the Navigation Controller
/// via Navigation events.
/// </summary>
public class Context{
    public int PrimaryScene = -1;
    public int BufferScene = -1;
}

public class MainContext : Context{
    public MainContext(){
        PrimaryScene = Scenes.MainView;
        BufferScene = Scenes.MainWorld;
    }
}

public class OtherContext : Context
{
    public OtherContext()
    {
        PrimaryScene = Scenes.OtherView;
        BufferScene = Scenes.OtherWorld;
    }
}