using UnityEngine;
using UnityEngine.UI;

public class UnitInfoView : PopupView {

    public void StackPopupOverUnit(PopupAsset popup, GameObject unit)
    {
        var form = Instantiate(popup.Popup).GetComponent<UnitInfoPopupForm>();
        var unitInfo = unit.GetComponent<Unit>();

        form.UnitNameText.text = unitInfo.Name;
        form.MovementText.text = string.Format("Remaining Movement: {0}", unitInfo.RemainingMovement);

        _popupContainer.Push(form.gameObject);
        _popupContainer.Peek().transform.SetParent(transform, false);

        var objectPos = unit.transform.position;
        var objectRenderrer = unit.GetComponent<MeshRenderer>();
        if (objectRenderrer)
        {
            objectPos.y += objectRenderrer.bounds.max.y;
            objectPos.z += objectRenderrer.bounds.extents.z;
        }
        var cam = FindObjectOfType<Camera>();
        form.transform.position = cam.WorldToScreenPoint(objectPos);

        var modPos = form.transform.position;
        modPos.x += 10;
        modPos.y += 10;
        form.transform.position = modPos;
    }
}
