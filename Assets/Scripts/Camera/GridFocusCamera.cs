using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OrbitCamera))]
public class GridFocusCamera : MonoBehaviour {

    private GameObject _focus;

    private void Start()
    {
        _focus = new GameObject("CameraFocus");
        EventSystem.instance.Connect<GridEvents.GridConstructedEvent>(PositionCamera);
    }

    private void OnDestroy()
    {
        EventSystem.instance.Disconnect<GridEvents.GridConstructedEvent>(PositionCamera);
    }

    private void PositionCamera(GridEvents.GridConstructedEvent e)
    {
        var newPosition = e.GridCenter;
        newPosition.x += e.Columns % 2 == 0 ? (e.TileSize * (float)(e.Columns - 1) / 2f) : (e.TileSize * (float)e.Columns / 2f);
        newPosition.y += 5f;

        transform.position = newPosition;
        transform.LookAt(e.GridCenter, Vector3.up);
        _focus.transform.position = e.GridCenter;
        GetComponent<OrbitCamera>().target = _focus.transform;
    }
}
