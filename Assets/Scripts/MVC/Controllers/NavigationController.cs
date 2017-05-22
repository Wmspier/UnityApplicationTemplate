using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationController : Controller {

    private Stack<Context> _contextStack = new Stack<Context>();

    public NavigationController()
    {
        EventSystem.instance.Connect<LoadContextEvent>(OnLoadContext);

        var e = new LoadContextEvent(new MainContext());
        EventSystem.instance.Dispatch(e);
    }

    public void OnLoadContext(LoadContextEvent e)
    {
        LoadContext(e.Context); 
    }

    private void LoadContext(Context context)
    {
        if (_contextStack.Count > 0)
        {
            var c = new Context();
            if (_contextStack.Peek().WorldScene != context.WorldScene)
            {
                SceneManager.UnloadSceneAsync(_contextStack.Peek().WorldScene);
                SceneManager.LoadScene(context.WorldScene);
                c.WorldScene = context.WorldScene;
            }
            else
                c.WorldScene = _contextStack.Peek().WorldScene;

            if (_contextStack.Peek().ViewScene != context.ViewScene)
            {
                SceneManager.UnloadSceneAsync(_contextStack.Peek().ViewScene);
                SceneManager.LoadScene(context.ViewScene, LoadSceneMode.Additive);
                c.ViewScene = context.ViewScene;
            }
            else
                c.ViewScene = _contextStack.Peek().ViewScene;
            Debug.Log("Pushing context into stack" + c.WorldScene + " | " + c.ViewScene);
            _contextStack.Push(c);
        }
        else
        {
            SceneManager.LoadScene(context.WorldScene);
            SceneManager.LoadScene(context.ViewScene, LoadSceneMode.Additive);
            _contextStack.Push(context);
        }
    }

    public void OnLoadPreviousContext()
    { 
        if (_contextStack.Count > 0)
        {
            LoadContext(_contextStack.Pop());
        }
        else
        {
            Debug.LogError("There is no previous context to load!");
        }
    }
}
