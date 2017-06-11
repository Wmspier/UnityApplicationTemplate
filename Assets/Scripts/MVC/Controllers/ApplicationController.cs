using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationController : Controller
{
    public ApplicationController()
	{
		EventSystem.instance.Connect<ApplicationEvents.StartUpFinishedEvent>(OnApplicationStart);
    }

    public void OnApplicationStart(ApplicationEvents.StartUpFinishedEvent e)
	{
        //Create a load screen event and dispatch it to be picked up by NavicationController
        var LoadScreenEvent = new NavigationEvents.LoadScreenEvent();
        LoadScreenEvent.Id = "MAIN";
        EventSystem.instance.Dispatch(LoadScreenEvent);
        Debug.Log("MADE IT");
    }
}
