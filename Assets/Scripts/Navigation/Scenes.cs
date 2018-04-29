/// <summary>
/// This list of scenes references the Scenes to Build within
/// the Unity Build Settings and must match exactly.  
/// 
/// They are intended to be used by Contexts to reference the
/// appropriate world and view scenes to load. 
/// </summary>
public static class Scenes {

    public static readonly int Invalid = -1;
    public static readonly int MainWorld = 0;
    public static readonly int MainView = 1;
    public static readonly int OtherWorld = 2;
    public static readonly int OtherView = 3;
    public static readonly int GridWorld = 4;
    public static readonly int GridView = 5;
}
