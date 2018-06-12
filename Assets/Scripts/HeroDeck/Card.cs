using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
     , IPointerEnterHandler
     , IPointerExitHandler
     , IPointerDownHandler
     , IPointerUpHandler
{

    public enum State
    {
        Idle,
        Preview,
        Selected
    }

    public CardModel Data { get; private set; }
    private CardView _view;
    
    private State _state;

    private bool _mouseFocus;
    private bool _clickFocus;
    private float _mouseFocusDuration;
    private float _mousePressLimit = 1f;

    private void Awake()
    {
        _state = State.Idle;
    }

    private void Update()
    {

        //  Provide logic here that does not require Input.
        switch(_state)
        {
            case State.Idle:
                {
                    if(_mouseFocus)
                    {
                        if (_mouseFocusDuration >= 0.1f && _state == State.Idle)
                        {
                           // ChangeState(State.Preview);
                        }
                        _mouseFocusDuration += Time.deltaTime;
                    }
                }
                break;
            case State.Preview:
                {
                    //  Look pretty~
                }
                break;
            case State.Selected:
                {
                    //  Slide along mouse
                    var position = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                    transform.position = position;
                }
                break;
        }
    }
    #region Pointer Logic
    public void OnPointerEnter(PointerEventData eventData)
    {
        _mouseFocus = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _mouseFocus = false;
        _mouseFocusDuration = 0f;
      //  ChangeState(State.Idle);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_mouseFocus)
        {
            if (_mouseFocusDuration >= 0.1f && !_clickFocus)
            {
                _clickFocus = true;
                ChangeState(State.Selected);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (_state)
        {
            case State.Preview:
                {
                    ChangeState(State.Idle);
                }
                break;
            case State.Selected:
                {
                    //  TODO aherrera : RAYCAST! PLAY CARD!

                    ChangeState(State.Idle);
                }
                break;
        }
    }
    #endregion

    public void InitializeData(CardModel data)
    {
        Data = data;
    }

    public void InitializeView()
    {
        _view = GetComponent<CardView>();
        _view.NameText.text = Data.Name;
        _view.DescriptionText.text = Data.Description;
        _view.HeroismText.text = Data.Cost.ToString();
    }

    private void ChangeState(State newState)
    {
        if(_state == newState)
        {
            return;
        }

        Debug.Log(string.Format("Changing state from {0} to {1}", _state.ToString(), newState.ToString()));

        switch (_state)
        {
            case State.Preview:
                {
                    OnExitStatePreview();
                }
                break;
            case State.Selected:
                {
                    OnExitStateSelect();
                }
                break;
            default:
                {
                    Debug.LogWarning("No implementation for change from this state: Card, " + _state.ToString());
                }
                break;
        }

        switch(newState)
        {
            case State.Preview:
                {
                    OnEnterStatePreview();
                }
                break;
            case State.Selected:
                {
                    OnEnterStateSelect();
                }
                break;
            default:
                {
                    Debug.LogWarning("No implementation for change into this state: Card, " + _state.ToString());
                }
                break;
        }

        _state = newState;
    }

    private void OnEnterStatePreview()
    {
        EventSystem.instance.Dispatch(new CardEvents.CardPreviewEvent(this));

        transform.localScale = Vector2.one * 5f;
    }
    private void OnExitStatePreview()
    {
        transform.localScale = Vector2.one;
        EventSystem.instance.Dispatch(new CardEvents.HideCardPreviewEvent(this));
    }

    private void OnEnterStateSelect()
    {
        EventSystem.instance.Dispatch(new CardEvents.CardSelectEvent(this));
    }

    private void OnExitStateSelect()
    {
        EventSystem.instance.Dispatch(new CardEvents.CardDeselectEvent(this));
    }
}
