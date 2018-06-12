using System;
using System.Collections;
using System.Collections.Generic;
using CardEvents;
using UnityEngine;

public class HandView : View
{
    [SerializeField]
    private RectTransform _handLayoutRoot;

    private Transform _baseRoot;
    
    private Card _previewingCard;
    private Card _selectedCard;

    private void Awake()
    {
        _baseRoot = this.transform;

        EventSystem.instance.Connect<DeckEvents.AddCardToHandEvent>(OnAddCardToHand);
        EventSystem.instance.Connect<CardEvents.CardPreviewEvent>(OnCardPreview);
        EventSystem.instance.Connect<CardEvents.HideCardPreviewEvent>(OnHideCardPreview);
        EventSystem.instance.Connect<CardEvents.CardSelectEvent>(OnCardSelect);
        EventSystem.instance.Connect<CardEvents.CardDeselectEvent>(OnCardDeselect);
    }

    private void OnDestroy()
    {
        EventSystem.instance.Disconnect<DeckEvents.AddCardToHandEvent>(OnAddCardToHand);
        EventSystem.instance.Disconnect<CardEvents.CardPreviewEvent>(OnCardPreview);
        EventSystem.instance.Disconnect<CardEvents.HideCardPreviewEvent>(OnHideCardPreview);
        EventSystem.instance.Disconnect<CardEvents.CardSelectEvent>(OnCardSelect);
        EventSystem.instance.Disconnect<CardEvents.CardDeselectEvent>(OnCardDeselect);
    }

    private void OnAddCardToHand(DeckEvents.AddCardToHandEvent e)
    {
        e.Card.transform.SetParent(_handLayoutRoot);
    }

    private void OnCardPreview(CardEvents.CardPreviewEvent e)
    {
        if(_previewingCard == null)
        {
            _previewingCard = e.Card;
            //  TODO aherrera : save child index
            _previewingCard.transform.SetParent(_baseRoot);
        }
    }

    private void OnHideCardPreview(CardEvents.HideCardPreviewEvent e)
    {
        if (_previewingCard != null && _previewingCard == e.Card)
        {
            _previewingCard = null;
            e.Card.transform.SetParent(_handLayoutRoot);
        }
    }

    private void OnCardSelect(CardEvents.CardSelectEvent e)
    {
        if(_selectedCard == null)
        {
            _selectedCard = e.Card;
            _selectedCard.transform.SetParent(_baseRoot);
        }
    }

    private void OnCardDeselect(CardDeselectEvent e)
    {
        if (_selectedCard != null && _selectedCard == e.Card)
        {
            _selectedCard = null;
            e.Card.transform.SetParent(_handLayoutRoot);
        }
    }
}
