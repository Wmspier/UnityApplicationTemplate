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

    public float GridWidth{
        get { return _tilePrefab.GetComponent<MeshRenderer>().bounds.size.x * Columns; }
    }
    public float GridLength
    {
        get { return _tilePrefab.GetComponent<MeshRenderer>().bounds.size.z * Rows; }
    }

    private void Awake()
    {
        //  Applying GridSettings to GridModel -- move to event for controller
        GridModel gridModel = ApplicationFacade.instance.GetModel<GridModel>();
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
        GridModel gridModel = ApplicationFacade.instance.GetModel<GridModel>();
        float tileSize = 0f;
        Vector3 center = new Vector3();
        for (int c = 0; c < Rows; ++c)
        {
            for (int r = 0; r < Columns; ++r)
            {
                var newTile = Instantiate(_tilePrefab, _gridRoot.transform).GetComponent<Tile>();
                newTile.name = string.Format("Tile c{0},r {1}", r, c);
                newTile.RowNumber = r;
                newTile.ColumnNumber = c;
                newTile.gameObject.AddComponent<InteractiveTileDebug>();

                tileSize = newTile.GetComponent<MeshRenderer>().bounds.size.x;

                newTile.transform.position = new Vector3(r * tileSize, 0f, c * tileSize);
            }
        }

        center = transform.position;
        center.x += Columns % 2 == 0 ? (tileSize * (float)(Columns - 1) / 2f) : (tileSize * (float)Columns / 2f);
        center.z += Rows % 2 == 0 ? (tileSize * (float)(Rows - 1) / 2f) : (tileSize * (float)Rows / 2f);

        var gridConstructedEvent = new GridEvents.GridConstructedEvent(
            rows: gridModel.Rows,
            columns: gridModel.Columns,
            size: tileSize,
            center: center
        );
        EventSystem.instance.Dispatch(gridConstructedEvent);
    }
}
