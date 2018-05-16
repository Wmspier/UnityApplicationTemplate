using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class Startup
{
    [RuntimeInitializeOnLoadMethod]
    static void OnLoad()
    {
        var main = new GameObject("[MAIN]");

        //ApplicationFacade for managing MVC
        var app = main.AddComponent<ApplicationFacade>();
        //EventSystem for managing GUI events
        main.AddComponent<UnityEngine.EventSystems.EventSystem>();
        //InputModule for handling input
        main.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
        //Asset Database for loading all assets within 'Resources'
        main.AddComponent<AssetDatabase>();
        //Register Models and Controllers
        RegisterMVC(ref app);
        //Make main a persistant object
        Object.DontDestroyOnLoad(main);
    }

    /// <summary>
    /// Registers all model and view classes for the application
    /// by storing them in maps owned by the ApplicationFacade
    /// </summary>
    /// <param name="app">A reference to an ApplicationFacade component</param>
    static void RegisterMVC(ref ApplicationFacade app)
    {
        //Need to register models first because they're referecned in Controller Constructors
        //TODO - fix this
        app.RegisterModel<GameModel>();
        app.RegisterModel<GridModel>();

        app.RegisterController<NavigationController>();
        app.RegisterController<GridController>();
        app.RegisterController<GridMovementController>();
        app.RegisterController<GridPopupController>();
    }
}
