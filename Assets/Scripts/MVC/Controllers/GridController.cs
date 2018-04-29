using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : Controller
{
    public GridController()
    {
        EventSystem.instance.Connect<GridEvents.CreateDataEvent>(CreateGrid);
    }

    public void CreateGrid(GridEvents.CreateDataEvent e)
    {
        GridModel gridModel = (GridModel)ApplicationFacade.instance.GetModel<GridModel>();

        gridModel.ConstructGridMatrix();

        var gridEvent = new GridEvents.ConstructGridEvent();
        EventSystem.instance.Dispatch(gridEvent);
    }

}
