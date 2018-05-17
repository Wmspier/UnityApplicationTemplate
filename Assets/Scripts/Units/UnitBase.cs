using System.Collections.Generic;
using System;
using System.Linq;
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

    public bool Selected{
        get 
        {
            return _selected;
        }
        set 
        {
            _selected = value;
            if(_selected)
                EventSystem.instance.Dispatch(new UnitEvents.UnitSelectedEvent(this));
        }
    }

    public UnitState State{
        get { return _state; }
        set 
        {
            _state = value;
            EventSystem.instance.Dispatch(new UnitEvents.UnitStateChangeEvent(value));
        }
    }
    public LineRenderer MovementLine;

    private UnitState _state = UnitState.Unselected;
    private bool _selected;
    private string _name;
    private int _maxMovement = 5;
    private int _remainingMovement;

    private List<BaseUnitBehavior> _behaviors;

    private float _dragSensitivity = 1f;
    private float _mouseDragDistance;
    private Vector3 _mouseDownPosition;

    protected void Awake()
    {
        MovementLine = GetComponent<LineRenderer>();
        RemainingMovement = MaxMovement;

        _behaviors = new List<BaseUnitBehavior> (GetComponents<BaseUnitBehavior>());
    }

    public void DeSelect()
    {
        State = UnitState.Unselected;
    }

    public void MoveToTarget(int tilesToTarget, float distance, Tile target)
    {
        var movememnt = GetBehavior<UnitMovement>();
        if(movememnt)
        {
            State = UnitState.Moving;
            movememnt.MoveToTarget(tilesToTarget, distance, target);
        }
    }

    public T GetBehavior<T>() where T : BaseUnitBehavior
    {
        return _behaviors.FirstOrDefault(b => b.GetType().IsAssignableFrom(typeof(T))) as T;
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
