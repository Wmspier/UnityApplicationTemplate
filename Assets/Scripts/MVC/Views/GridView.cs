using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridView : View
{
    public void OnCreateGridButton()
    {
        var gridEvent = new GridEvents.CreateDataEvent();
        EventSystem.instance.Dispatch(gridEvent);
    }
}
