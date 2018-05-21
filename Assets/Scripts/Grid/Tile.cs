using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
public class Tile : MonoBehaviour
{
    public int RowNumber { get; set; }
    public int ColumnNumber { get; set; }

    public GameObject SelectedObject;

    public bool Traversable { 
        get{
            return _traversable;
        }
        set{
            _traversable = value;
            var matInstance = _renderer.material;
            matInstance.color = _traversable ? Color.white : Color.gray;
            _renderer.material = matInstance;
        }
    }
    public bool Selected {
        get {
            return _selected;
        }
        set {
            _selected = value;
            if(SelectedObject != null)
            {
                SelectedObject.SetActive(_selected);
            }
        }
    }
    public bool Occupied {
        get {
            return _occupyingUnit != null;
        }
    }
    public Unit OccupyingUnit {
        get {
            return _occupyingUnit;
        }
    }

    private bool _traversable;
    private bool _selected;
    private MeshRenderer _renderer;
    private Unit _occupyingUnit;

    public void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        Traversable = true;
        Selected = false;
        EventSystem.instance.Connect<GridEvents.TileSelectedEvent>(OnTileSelected);
    }
    public void OnDestroy()
    {
        EventSystem.instance.Disconnect<GridEvents.TileSelectedEvent>(OnTileSelected); 
    }

    public void OnTileSelected(GridEvents.TileSelectedEvent e)
    {
        Selected &= e.SelectedTile == this;
    }

    public void Occupy(Unit unit)
    {
        _occupyingUnit = unit;
    }

    public void UnOccupy(){
        _occupyingUnit = null;
    }
}
