using UnityEngine;

public class UnitMovement : BaseUnitBehavior {

    private float _lerpToTileSpeed = 0.25f;
    private float _lerpLength;
    private float _lerpStartTime;
    private int _tilesToTarget;
    private Vector3 _lerpStart;
    private Vector3 _lerpEnd;

    private Tile _moveTargetTile;

    public void MoveToTarget(int tilesToTarget, float distance, Tile target)
    {
        _tilesToTarget = tilesToTarget;
        _lerpLength = distance;
        _lerpStartTime = Time.time;
        _moveTargetTile = target;
        _lerpStart = transform.position;
        _lerpEnd = _moveTargetTile.transform.position;
        _lerpEnd.y += GetComponent<MeshRenderer>().bounds.extents.y;
    }

    private void Update()
    {
        if (_moveTargetTile != null && Owner.OccupyingTile != _moveTargetTile)
        {
            Debug.Log(_tilesToTarget * _lerpToTileSpeed);
            float distCovered = (Time.time - _lerpStartTime) * (_tilesToTarget * (1 - _lerpToTileSpeed));
            ; float fracJourney = distCovered / _lerpLength;
            transform.position = Vector3.Lerp(_lerpStart, _lerpEnd, fracJourney);
            if (transform.position.Equals(_lerpEnd))
            {
                Owner.OccupyingTile = _moveTargetTile;
                Owner.State = Unit.UnitState.Unselected;
            }
        }
    }
}
