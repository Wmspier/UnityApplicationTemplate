using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(LineRenderer))]
public class Unit : MonoBehaviour {

    public UnitQuickInfoView InfoPanel;

    public enum UnitState {
        Idle,
        Targeting,
        Moving
    }
    public Tile OccupyingTile { 
        get{
            return _occupyingTile;
        }
        set{
            if(_occupyingTile)
                _occupyingTile.UnOccupy();
            _occupyingTile = value;
            _occupyingTile.Occupy(this);
        }
    }
    public string Name {
        get {
            return _name;
        }
        set {
            _name = value;
            name = _name;
        }
    }

    public bool Selected {
        get {
            return _selected;
        }
        set {
            _selected = value;
            if (_selected)
                EventSystem.instance.Dispatch(new UnitEvents.UnitSelectedEvent(this));
        }
    }

    public UnitState State {
        get { 
            return _state; 
        }
        set {
            _state = value;
            EventSystem.instance.Dispatch(new UnitEvents.UnitStateChangeEvent(value, this));
        }
    }

    public int Health {
        get {
            return _health;
        }
        set {
            _health = value;
            if( _health <= 0) {
                _health = 0;
                Die();
            }
        }
    }

    public int Power {
        get {
            return _power;
        }
        set {
            _power = value;
        }
    }

    public int Armor {
        get {
            return _armor;
        }
        set {
            _armor = value;
            if (_armor < 0)
                _armor = 0;
        }
    }
        

    private UnitState _state = UnitState.Idle;
    private bool _selected;
    private string _name;
    private Tile _occupyingTile;

    private List<BaseUnitBehavior> _behaviors;

    private float _dragSensitivity = 1f;
    private float _mouseDragDistance;
    private float _mouseHoldDuration;
    private float _mousePressLimit = 1f;
    private Vector3 _mouseDownPosition;
    private bool _mouseFocus;
    private bool _lostFocusOnDrag;

    private int _health = 1;
    private int _power = 1;
    private int _armor = 1;

    protected void Awake()
    {
        HideInfoPanel();
        _behaviors = new List<BaseUnitBehavior>(GetComponents<BaseUnitBehavior>());
    }

    public void MoveToTarget(int tilesToTarget, float distance, Tile target)
    {
        var movememnt = GetBehavior<Movement>();
        if (movememnt && !target.Occupied)
        {
            State = UnitState.Moving;
            movememnt.MoveToTarget(tilesToTarget, distance, target);
        }
    }

    public void ShowInfoPanel(bool ForMovement = false)
    {
        InfoPanel.Initialize(Armor, Power, Health, GetBehavior<Movement>().RemainingMovement);
        InfoPanel.gameObject.SetActive(true);
        if (ForMovement)
            InfoPanel.ShowMovement();
        else
            InfoPanel.ShowCombatStats();
    }
    public void HideInfoPanel()
    {
        InfoPanel.gameObject.SetActive(false);
    }

    public T GetBehavior<T>() where T : BaseUnitBehavior
    {
        return _behaviors.FirstOrDefault(b => b.GetType().IsAssignableFrom(typeof(T))) as T;
    }

    private void Die()
    {
        EventSystem.instance.Dispatch(new UnitEvents.UnitKilledEvent(this));
        OccupyingTile.UnOccupy();
        Destroy(this.gameObject);
    }

    private void OnMouseUpAsButton()
    {
        if (!Selected && !_lostFocusOnDrag && _mouseHoldDuration <= _mousePressLimit)
        {
            Selected = true;
            EventSystem.instance.Dispatch(new UnitEvents.UnitSelectedEvent(this));
        }
        if (State == UnitState.Moving)
            Selected = false;
    }

    private void OnMouseDrag()
    {
        _mouseDragDistance = (_mouseDownPosition - Input.mousePosition).magnitude;
        if ((_mouseDragDistance >= _dragSensitivity) && State != UnitState.Moving && !Selected && _mouseFocus)
        {
            State = UnitState.Targeting;
            EventSystem.instance.Dispatch(new UnitEvents.UnitMoveEvent(this));
        }
    }

    private void OnMouseDown()
    {
        _mouseDownPosition = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        if (State == UnitState.Targeting)
            State = UnitState.Idle;
        _lostFocusOnDrag = false;
        _mouseHoldDuration = 0f;
    }

    private void OnMouseOver()
    {
        _mouseFocus = true;
    }
    private void OnMouseExit()
    {
        _mouseFocus = false;
        _lostFocusOnDrag = true;
        if (State == UnitState.Idle)
            HideInfoPanel();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && _mouseFocus)
        {
            if (_mouseHoldDuration >= 0.1f)
                ShowInfoPanel();
            _mouseHoldDuration += Time.deltaTime;
        }
    }
}
