/// <summary>
/// Contexts are references to two scenes (World and View).
/// 
/// They are intended to be loaded by the Navigation Controller
/// via Navigation events.
/// </summary>
public class Context{
    public int WorldScene = -1;
    public int ViewScene = -1;
}

public class MainContext : Context{
    public MainContext(){
        WorldScene = Scenes.MainWorld;
        ViewScene = Scenes.MainView;
    }
}

public class OtherContext : Context
{
    public OtherContext()
    {
        WorldScene = Scenes.OtherWorld;
        ViewScene = Scenes.OtherView;
    }
}

public class GridContext : Context
{
    public GridContext()
    {
        WorldScene = Scenes.GridWorld;
        ViewScene = Scenes.GridView;
    }
}