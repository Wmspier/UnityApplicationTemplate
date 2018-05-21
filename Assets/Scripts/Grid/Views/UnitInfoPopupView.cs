using UnityEngine;
using UnityEngine.UI;

public class UnitInfoView : PopupView {

    public void StackPopupOverUnit(PopupAsset popup, GameObject unitObject)
    {
        var form = Instantiate(popup.Popup).GetComponent<UnitInfoPopupForm>();
        var unit = unitObject.GetComponent<Unit>();

        form.UnitNameText.text = unit.Name;
        form.MovementText.text = string.Format("Remaining Movement: {0}", unit.GetBehavior<Movement>().RemainingMovement);

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

    public void StackUnitPopup(PopupAsset popup, GameObject unitObject)
    {
        var form = Instantiate(popup.Popup).GetComponent<UnitInfoPopupForm>();
        var unit = unitObject.GetComponent<Unit>();
        form.transform.SetParent(transform);
        form.gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
        form.gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);

        form.UnitNameText.text = unit.Name;
        form.MovementText.text = unit.GetBehavior<Movement>().RemainingMovement.ToString();
        form.HealthText.text = unit.Health.ToString();
        form.PowerText.text = unit.Power.ToString();
        form.ArmorText.text = unit.Armor.ToString();
        if (unit.Armor == 0)
            form.ArmorObject.SetActive(false);

        _popupContainer.Push(form.gameObject);
    }
}
