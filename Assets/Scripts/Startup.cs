using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
class Startup
{
    [RuntimeInitializeOnLoadMethod]
    static void OnLoad()
    {
        var main = new GameObject("[MAIN]");
        main.AddComponent<ApplicationFacade>();
        Object.DontDestroyOnLoad(main);
    }
}
