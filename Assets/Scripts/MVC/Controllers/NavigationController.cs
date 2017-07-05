using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.EventSystem;
using NavigationEvents;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationController : Controller {

    private Stack<Context> _historyStack = new Stack<Context>();
    private Context _currentContext;

    private Stack<GameObject> _screenStack = new Stack<GameObject>();

    public NavigationController()
    {
        EventSystem.Instance.Connect<LoadScreenEvent>(OnLoadScreen);
        EventSystem.Instance.Connect<UnloadScreen>(OnUnloadScreen);

		//var event1 = new NavigationEvents.LoadContextEvent(new MainContext(), true);
        //EventSystem.instance.Dispatch(event1);
    }

	public void OnLoadScreen(LoadScreenEvent e)
	{
	    var args = (LoadScreenArgs)e.Args;
        var NewScreenAsset = AssetDatabase.instance.GetAsset<BasicScreenAsset>(args.Id);
        var NewScreen = GameObject.Instantiate(NewScreenAsset.ScreenPrefab);
        NewScreen.name = NewScreenAsset.Id;
        _screenStack.Push(NewScreen);
	}

	public void OnUnloadScreen(NavigationEvents.UnloadScreen e)
	{
        if (_screenStack.Count == 0) return;
        GameObject.Destroy(_screenStack.Pop());
	}

    private void LoadContext(Context context, bool back)
    {
        if (_currentContext != null)
        {
            var c = new Context();
            if (_currentContext.PrimaryScene != context.PrimaryScene)
            {
                SceneManager.UnloadSceneAsync(_currentContext.PrimaryScene);
                SceneManager.LoadScene(context.PrimaryScene);
                c.PrimaryScene = context.PrimaryScene;
            }
            else
                c.PrimaryScene = _currentContext.PrimaryScene;

            if (_currentContext.BufferScene != context.BufferScene)
            {
                SceneManager.UnloadSceneAsync(_currentContext.BufferScene);
                SceneManager.LoadScene(context.BufferScene, LoadSceneMode.Additive);
                c.BufferScene = context.BufferScene;
            }
            else
                c.BufferScene = _currentContext.BufferScene;
            
            if(back)
                _historyStack.Push(_currentContext);
            _currentContext = c;
        }
        else
        {
            SceneManager.LoadScene(context.PrimaryScene);
            SceneManager.LoadScene(context.BufferScene, LoadSceneMode.Additive);

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
