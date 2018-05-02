using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridConstructor : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    private GameObject _tilePrefab;

    [Header("Object Instances")]
    [SerializeField]
    private Transform _gridRoot;

    [Header("Grid Settings")]
    public int Rows = 1;
    public int Columns = 1;
    public Vector2 CellDimensions = Vector2.one;

    private void Awake()
    {
        //  Applying GridSettings to GridModel -- move to event for controller
        GridModel gridModel = (GridModel)ApplicationFacade.instance.GetModel<GridModel>();
        gridModel.Rows = Rows;
        gridModel.Columns = Columns;
    }

    private void Start()
    {
        EventSystem.instance.Connect<GridEvents.ConstructGridEvent>(InstantiateGrid);
    }

    private void OnDestroy()
    {
        EventSystem.instance.Disconnect<GridEvents.ConstructGridEvent>(InstantiateGrid);
    }

    public void InstantiateGrid(GridEvents.ConstructGridEvent e)
    {
        GridModel gridModel = (GridModel)ApplicationFacade.instance.GetModel<GridModel>();
        for (int c = 0; c < gridModel.Rows; ++c)
        {
            for (int r = 0; r < gridModel.Columns; ++r)
            {
                var newTile = Instantiate(_tilePrefab, _gridRoot.transform);
                newTile.name = string.Format("Tile c{0},r {1}", r, c);
                newTile.AddComponent<TileView>().InitializeTile(r, c);

                newTile.transform.position = new Vector2(r * CellDimensions.x, c * CellDimensions.y);
            }
        }
    }


}
