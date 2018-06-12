using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandModel : Model
{
    public List<CardModel> AvailableCards;

    public HandModel()
    {
        AvailableCards = new List<CardModel>();
    }

    public void Add(CardModel card)
    {
        //  TODO : handle hand limit

        AvailableCards.Add(card);
    }
}
