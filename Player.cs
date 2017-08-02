using System;
using System.Collections.Generic;

namespace speedwar
{
  class Player
  {
    public string name;
    public List<Card> hand = new List<Card>();
    public Card draw(Deck deck)
    {
      Card newCard = deck.deal();
      hand.Add(newCard);
      return newCard;
    }
    public Card discard(int index)
    {
      if (hand.Count > index)
      {
        Card discarded = hand[index];
        hand.RemoveAt(index);
        return discarded;
      }
      else {
        return null;
      }

    }
  }
}