using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
     , IPointerEnterHandler
     , IPointerExitHandler {

    public string Name;
    public string Description;
    public int Cost;

    private CardView _view;

    private bool _previewing;

    private bool _mouseFocus;
    private float _mouseHoldDuration;
    private float _mousePressLimit = 1f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _mouseFocus = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _mouseFocus = false;
    }

    private void Awake()
    {
        _view = GetComponent<CardView>();
        _view.NameText.text = Name;
        _view.DescriptionText.text = Description;
        _view.HeroismText.text = Cost.ToString();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && _mouseFocus)
        {
            if (_mouseHoldDuration >= 0.1f && !_previewing)
            {
                _previewing = true;
                EventSystem.instance.Dispatch(new CardEvents.CardPreviewEvent(this));
            }
            _mouseHoldDuration += Time.deltaTime;
        }
        else if(_previewing)
        {
            _previewing = false;
            EventSystem.instance.Dispatch(new CardEvents.HideCardPreviewEvent(this));
        }
    }
}
