using System.Collections.Generic;
using UnityEngine;
public class PopupView : View {

    protected Stack<GameObject> _popupContainer = new Stack<GameObject>();
    protected RectTransform _transform;

    protected void Awake()
    {
        _transform = GetComponent<RectTransform>();

        _transform.offsetMin = Vector2.zero;
        _transform.offsetMax = Vector2.zero;
        _transform.anchorMin = Vector2.zero;
        _transform.anchorMax = new Vector2(1, 1);
    }

    public virtual void StackPopup(PopupAsset popup)
    { 
        _popupContainer.Push(Instantiate(popup.Popup));
        _popupContainer.Peek().transform.SetParent(transform, false);
    }

    public void ClosePopup()
    {
        var top = _popupContainer.Pop();
        if(top)
            Destroy(top);
    }
}
