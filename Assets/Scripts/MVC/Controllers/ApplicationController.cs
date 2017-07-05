using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.EventSystem;
using NavigationEvents;
using UnityEngine;

public class ApplicationController : Controller
{
    public ApplicationController()
	{
		EventSystem.Instance.Connect<ApplicationEvents.StartUpFinishedEvent>(OnApplicationStart);
    }

    public void OnApplicationStart(ApplicationEvents.StartUpFinishedEvent e)
	{
        //Create a load screen event and dispatch it to be picked up by NavicationController
        var LoadScreenEvent = new LoadScreenEvent(new LoadScreenArgs("MAIN"));
        //EventSystem.Instance.Dispatch(LoadScreenEvent);
        EventSystem.Instance.QueueEvent(LoadScreenEvent);
    }
}
