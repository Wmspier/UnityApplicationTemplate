using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovementController : IController {

    private Unit _movingUnit;
    private Tile _unitTarget;

    public GridMovementController()
    {
        EventSystem.instance.Connect<UnitEvents.UnitMoveEvent>(OnUnitMoved);
    }

	public void Update()
    {
        if(_movingUnit != null && _movingUnit.GetComponent<Movement>())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                if (objectHit.tag == "Tile")
                {
                    _unitTarget = objectHit.GetComponent<Tile>();
                    _movingUnit.ShowInfoPanel(true);
                    _movingUnit.GetBehavior<Targeting>().DrawLineToTile(_unitTarget);
                }
            }
            if(Input.GetMouseButtonUp(0))
            {
                var distanceToTarget = GridUtility.GetDistanceBetweenTiles(_movingUnit.OccupyingTile, _unitTarget);

                if (distanceToTarget == 0)
                {
                    _movingUnit.Selected = false;
                }
                else if(distanceToTarget <= _movingUnit.GetBehavior<Movement>().RemainingMovement)
                {
                    _movingUnit.MoveToTarget(distanceToTarget, (_movingUnit.OccupyingTile.transform.position - _unitTarget.transform.position).magnitude, _unitTarget);
                    _movingUnit.GetBehavior<Movement>().RemainingMovement -= distanceToTarget;
                }

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
}
