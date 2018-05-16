using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPopupController : PopupController
{
    private GridModel _gridModel;

    public GridPopupController() 
    {
        _gridModel = ApplicationFacade.instance.GetModel<GridModel>();

        EventSystem.instance.Connect<PopupEvents.OpenPopupAboveUnitEvent>(OnPopupOpenOverUnit);
        EventSystem.instance.Connect<PopupEvents.ClosePopupEvent>(OnPopupClose);
    }

    ~GridPopupController()
    {
        EventSystem.instance.Disconnect<PopupEvents.OpenPopupAboveUnitEvent>(OnPopupOpenOverUnit);
        EventSystem.instance.Disconnect<PopupEvents.ClosePopupEvent>(OnPopupClose);
    }

    private void OnPopupOpenOverUnit(PopupEvents.OpenPopupAboveUnitEvent e)
    {
        if (View == null) 
            InstantiateView();

        _gridModel.SelectedUnit = e.Unit.GetComponent<Unit>();

        View.GetComponent<UnitInfoView>().StackPopupOverUnit(e.Popup, e.Unit);
    }
    protected new void InstantiateView()
    {
        var viewRoot = GameObject.FindWithTag("PopupRoot");
        if (viewRoot == null)
            viewRoot = UnityEngine.Object.FindObjectOfType<Canvas>().gameObject;

        View = new GameObject("GridPopupView");
        View.AddComponent<Canvas>();
        View.AddComponent<UnitInfoView>();
        View.transform.SetParent(viewRoot.gameObject.transform, false);
    }

    protected new void OnPopupClose(PopupEvents.ClosePopupEvent e)
    {
        if(_gridModel.SelectedUnit != null)
        {
            _gridModel.SelectedUnit.DeSelect();
            _gridModel.SelectedUnit = null;
        }
    }
}
