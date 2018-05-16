using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider), typeof(LineRenderer))]
public class Unit : MonoBehaviour {

    public enum UnitState
    {
        Unselected,
        Selected,
        BeginMove,
        Moving
    }

    public int MaxMovement { 
        get
        {
            return _maxMovement;
        }
        set{
            _maxMovement = value;
        }
    }
    public int RemainingMovement { 
        get{
            return _remainingMovement;
        } 
        set{
            _remainingMovement = value;
            if (_remainingMovement < 0)
                _remainingMovement = 0;
        } 
    }
    public Tile OccupyingTile { get; set; }
    public string Name { 
        get
        {
            return _name;
        } 
        set
        {
            _name = value;
            name = _name;
        }
    }

    public UnitState State{
        get { return _state; }
        set {
            _state = value;
            EventSystem.instance.Dispatch(new UnitEvents.UnitStateChangeEvent(value));
        }
    }
    public LineRenderer MovementLine;

    private UnitState _state = UnitState.Unselected;
    private string _name;
    private int _maxMovement = 5;
    private int _remainingMovement;
    private float _lerpToTileSpeed = 0.5f;
    private float _lerpLength;
    private float _lerpStartTime;
    private int _tilesToTarget;
    private Vector3 _lerpStart;
    private Vector3 _lerpEnd;

    private float _dragSensitivity = 1f;
    private float _mouseDragDistance;
    private Vector3 _mouseDownPosition;

    private Tile _moveTargetTile;

    protected void Awake()
    {
        MovementLine = GetComponent<LineRenderer>();
        RemainingMovement = MaxMovement;
    }

    public void DeSelect()
    {
        State = UnitState.Unselected;
    }

    public void MoveToTarget(int tilesToTarget, float distance, Tile target)
    {
        State = UnitState.Moving;

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
        if(State == UnitState.Moving && _moveTargetTile != null && OccupyingTile != _moveTargetTile)
        {
            float distCovered = (Time.time - _lerpStartTime) * (_tilesToTarget * _lerpToTileSpeed);
;           float fracJourney = distCovered / _lerpLength;
            transform.position = Vector3.Lerp(_lerpStart, _lerpEnd, fracJourney);
            if (transform.position.Equals(_lerpEnd))
            {
                OccupyingTile = _moveTargetTile;
                State = UnitState.Unselected;
            }
        }
    }

    private void OnMouseUpAsButton()
    {
        if(State == UnitState.Unselected)
        {
            State = UnitState.Selected;
            EventSystem.instance.Dispatch(new UnitEvents.UnitSelectedEvent(this));
        }
        if (State == UnitState.Moving)
            State = UnitState.Unselected;
    }

    private void OnMouseDrag()
    {
        _mouseDragDistance = (_mouseDownPosition - Input.mousePosition).magnitude;
        if ((_mouseDragDistance >= _dragSensitivity) && State != UnitState.Moving && State != UnitState.Selected)
        {
            State = UnitState.BeginMove;
            EventSystem.instance.Dispatch(new UnitEvents.UnitMoveEvent(this));
        } 
    }

    private void OnMouseDown()
    {
        _mouseDownPosition = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        if (State == UnitState.BeginMove)
            State = UnitState.Unselected;
    }
}
