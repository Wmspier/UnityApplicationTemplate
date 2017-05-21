/// <summary>
/// Contexts are references to two scenes (World and View).
/// 
/// They are intended to be loaded by the Navigation Controller
/// via Navigation events.
/// </summary>
public abstract class Context{
    protected static int WorldScene = -1;
    protected static int ViewScene = -1;
}

public class MainContext : Context{
    MainContext(){
        WorldScene = Scenes.Main;
        ViewScene = Scenes.MainView;
    }
}