using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDeckModel : Model
{
    public List<CardModel> DeckContents;

    public HeroDeckModel()
    {
        DeckContents = new List<CardModel>();
    }

    public CardModel DrawFromDeck()
    {
        //  TODO : handle empty deck condition

        return DeckContents.Pop();
    }
}
