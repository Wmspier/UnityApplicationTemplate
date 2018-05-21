using UnityEngine;

[RequireComponent(typeof(BezierLineRenderer), typeof(LineRenderer))]
public class Targeting : BaseUnitBehavior {

    public Material ArrowMaterial;
    public Material InteractingMaterial;

    private BezierLineRenderer _bLine;
    private LineRenderer _line;

    private Unit _target;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (_target != null) {
                if(_target != Owner) {
                    Owner.GetBehavior<Combat>().AttackUnit(_target);
                }
                _target.HideInfoPanel();
                _target = null;
            }
            _bLine.Clear();
            Owner.HideInfoPanel();
        }
    }

    public void DrawLineToTile(Tile target)
    {
        if (_target != null)
            _target.HideInfoPanel();
        
        EventSystem.instance.Dispatch(new GridEvents.TileSelectedEvent(target));
        if (target == Owner.OccupyingTile)
        {
            Owner.HideInfoPanel();
            _bLine.Clear();
            return;
        }

        if (GridUtility.IsWithinMoveRange(Owner, target) && !target.Occupied)
        {
            _line.material = ArrowMaterial;
            _line.startColor = Color.green;
            _line.endColor = Color.green;
            _target = null;
        }
        else
        {
            if (target.Occupied)
            {
                _target = target.OccupyingUnit;
                _line.material = InteractingMaterial;
                Owner.ShowInfoPanel();
                target.OccupyingUnit.ShowInfoPanel();
            }
            else
            {
                _line.material = ArrowMaterial;
                _target = null;
            }
            
            _line.startColor = Color.red;
            _line.endColor = Color.red;
        }
        var startPos = transform.position;

        startPos.y += GetComponent<MeshRenderer>().bounds.extents.y;
        _bLine.DrawLine(startPos, target.transform.position);
    }
}
