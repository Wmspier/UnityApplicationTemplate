using UnityEngine;

[RequireComponent(typeof(Targeting))]
public class Movement : BaseUnitBehavior 
{
    public int MaxMovement
    {
        get
        {
            return _maxMovement;
        }
        set
        {
            _maxMovement = value;
        }
    }
    public int RemainingMovement
    {
        get
        {
            return _remainingMovement;
        }
        set
        {
            _remainingMovement = value;
            if (_remainingMovement < 0)
                _remainingMovement = 0;
        }
    }

    private float _lerpToTileSpeed = 0.25f;
    private float _lerpLength;
    private float _lerpStartTime;
    private int _tilesToTarget;
    private Vector3 _lerpStart;
    private Vector3 _lerpEnd;

    private int _maxMovement = 5;
    private int _remainingMovement;

    private Tile _moveTargetTile;

    private new void Awake()
    {
        base.Awake();
        RemainingMovement = MaxMovement;
    }

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
            float distCovered = (Time.time - _lerpStartTime) * (_tilesToTarget * (1 - _lerpToTileSpeed));
            ; float fracJourney = distCovered / _lerpLength;
            transform.position = Vector3.Lerp(_lerpStart, _lerpEnd, fracJourney);
            if (transform.position.Equals(_lerpEnd))
            {
                Owner.HideInfoPanel();
                Owner.OccupyingTile = _moveTargetTile;
                Owner.State = Unit.UnitState.Idle;
            }
        }
    }
}
