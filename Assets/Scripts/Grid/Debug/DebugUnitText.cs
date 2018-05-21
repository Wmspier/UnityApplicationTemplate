using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUnitText : MonoBehaviour {

    private Unit _owner;

	void Awake () 
    {
        _owner = transform.parent.GetComponent<Unit>();
        EventSystem.instance.Connect<UnitEvents.UnitStateChangeEvent>(OnUnitStateChange);
	}
    private void OnDestroy()
    {
        EventSystem.instance.Disconnect<UnitEvents.UnitStateChangeEvent>(OnUnitStateChange);
    }

    // Update is called once per frame
    void Update () {
        transform.LookAt(Camera.main.transform);
	}

    void OnUnitStateChange(UnitEvents.UnitStateChangeEvent e)
    {
        if (e.Unit != _owner)
            return;
        
        var text = GetComponentInChildren(typeof(Text)) as Text;
        text.text = e.State.ToString();
    }
}
