using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tile), typeof(Collider))]
public class InteractiveTileDebug : MonoBehaviour {

    private Tile _tile;
    private bool _mouseOver;

	private void Awake () {
        _tile = GetComponent<Tile>();
	}

    private void OnMouseEnter()
    {
        _mouseOver = true;
    }

    private void OnMouseExit()
    {
        _mouseOver = false;
    }

    private void OnMouseUp()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            return;
        
        if(_mouseOver && Input.GetKey(KeyCode.T))
            _tile.Traversable = !_tile.Traversable;
        if(!Input.anyKey)
        {
            _tile.Selected = !_tile.Selected;
            EventSystem.instance.Dispatch(new GridEvents.TileSelectedEvent(_tile));
        }
    }
}
