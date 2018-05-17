
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : IController
{
    private GridModel _model;

    public GridController()
    {
        EventSystem.instance.Connect<GridEvents.CreateDataEvent>(OnCreateGrid);
        EventSystem.instance.Connect<GridEvents.TileSelectedEvent>(OnTileSelcted);

        _model = ApplicationFacade.instance.GetModel<GridModel>();
    }

    ~GridController()
    {
        EventSystem.instance.Disconnect<GridEvents.CreateDataEvent>(OnCreateGrid);
        EventSystem.instance.Disconnect<GridEvents.TileSelectedEvent>(OnTileSelcted);
    }

    public void Update() { }

    public void OnCreateGrid(GridEvents.CreateDataEvent e)
    {
        var gridEvent = new GridEvents.ConstructGridEvent();
        EventSystem.instance.Dispatch(gridEvent);
    }

    public void OnTileSelcted(GridEvents.TileSelectedEvent e)
    {
        if (_model.SelectedTile == e.SelectedTile)
            _model.SelectedTile = null;
        else
            _model.SelectedTile = e.SelectedTile;
    }
}
