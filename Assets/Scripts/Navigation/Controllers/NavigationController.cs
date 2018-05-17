using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationController : IController {

    private Stack<Context> _historyStack = new Stack<Context>();
    private Context _currentContext;

    public NavigationController()
    {
        EventSystem.instance.Connect<NavigationEvents.LoadContextEvent>(OnLoadContext);
        EventSystem.instance.Connect<NavigationEvents.PreviousContextEvent>(OnLoadPreviousContext);

        var event1 = new NavigationEvents.LoadContextEvent(new MainContext(), true);
        EventSystem.instance.Dispatch(event1);
    }

    public void Update(){}

    public void OnLoadContext(NavigationEvents.LoadContextEvent e)
    {
        LoadContext(e.Context, e.Back); 
    }

    public void OnLoadPreviousContext(NavigationEvents.PreviousContextEvent e)
    {
        LoadPreviousContext();
    }

    private void LoadContext(Context context, bool back)
    {
        if (_currentContext != null)
        {
            var c = new Context();
            if (_currentContext.WorldScene != context.WorldScene)
            {
                SceneManager.LoadScene(context.WorldScene);
                c.WorldScene = context.WorldScene;
            }
            else
                c.WorldScene = _currentContext.WorldScene;

            if (_currentContext.ViewScene != context.ViewScene)
            {
                SceneManager.LoadScene(context.ViewScene, LoadSceneMode.Additive);
                c.ViewScene = context.ViewScene;
            }
            else
                c.ViewScene = _currentContext.ViewScene;
            
            if(back)
                _historyStack.Push(_currentContext);
            _currentContext = c;
        }
        else
        {
            SceneManager.LoadScene(context.WorldScene);
            SceneManager.LoadScene(context.ViewScene, LoadSceneMode.Additive);

            _currentContext = context;
        }
    }

    private void LoadPreviousContext()
    { 
        if (_historyStack.Count > 0)
        {
            LoadContext(_historyStack.Pop(), false);
        }
        else
        {
            Debug.LogError("There is no previous context to load!");
        }
    }
}
