using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovementController : IController {

    private Unit _movingUnit;
    private Tile _unitTarget;
    //private GridModel _gridModel;

    public GridMovementController()
    {
        //_gridModel = ApplicationFacade.instance.GetModel<GridModel>();
        EventSystem.instance.Connect<UnitEvents.UnitMoveEvent>(OnUnitMoved);
    }

	public void Update()
    {
        if(_movingUnit != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                if (objectHit.tag == "Tile")
                {
                    _unitTarget = objectHit.GetComponent<Tile>();
                    DrawMovementLineToTile(_movingUnit, _unitTarget);
                }
            }
            if(Input.GetMouseButtonUp(0))
            {
                var distanceToTarget = GetDistanceBetweenTiles(_movingUnit.OccupyingTile, _unitTarget);

                if (distanceToTarget == 0)
                {
                    _movingUnit.DeSelect();
                }
                else if(distanceToTarget <= _movingUnit.RemainingMovement)
                {
                    _movingUnit.MoveToTarget(distanceToTarget, (_movingUnit.OccupyingTile.transform.position - _unitTarget.transform.position).magnitude, _unitTarget);
                    _movingUnit.RemainingMovement -= distanceToTarget;
                }
                
                _movingUnit.MovementLine.SetPosition(0, Vector3.zero);
                _movingUnit.MovementLine.SetPosition(1, Vector3.zero);
                _movingUnit = null;
                _unitTarget = null;
            }
        }
    }

    private void OnUnitMoved(UnitEvents.UnitMoveEvent e)
    {
        if (_movingUnit == null)
            _movingUnit = e.SelectedUnit;
    }

    private void DrawMovementLineToTile(Unit unit, Tile target)
    {
        if (unit.MovementLine.GetPosition(1) != target.transform.position)
        {
            if (IsWithinMoveRange(unit, target))
                unit.MovementLine.endColor = Color.green;
            else
                unit.MovementLine.endColor = Color.red;
            var startPos = unit.transform.position;

            startPos.y += unit.GetComponent<MeshRenderer>().bounds.extents.y;
            unit.MovementLine.SetPosition(0, startPos);
            unit.MovementLine.SetPosition(1, target.transform.position);
        }
    }

    private bool IsWithinMoveRange(Unit unit, Tile target)
    {
        return GetDistanceBetweenTiles(unit.OccupyingTile, target) <= unit.RemainingMovement;
    }

    private int GetDistanceBetweenTiles(Tile start, Tile end)
    {
        var unitPos = new Vector2(start.RowNumber, start.ColumnNumber);
        var targetPos = new Vector2(end.RowNumber, end.ColumnNumber);
        int dx = Mathf.Abs((int)targetPos.x - (int)unitPos.x);
        int dy = Mathf.Abs((int)targetPos.y - (int)unitPos.y);

        int min = Mathf.Min(dx, dy);
        int max = Mathf.Max(dx, dy);

        int diagonalSteps = min;
        int straightSteps = max - min;

        return (int)Mathf.Sqrt(2) * diagonalSteps + straightSteps;
    }
}
