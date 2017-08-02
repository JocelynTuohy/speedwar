using System;
using System.Collections.Generic;

namespace speedwar
{
  public class Hand:Deck{
    public Hand(){
      // constructor
      cards = new List<Card>();
    }
    
    public Card discard(int index)
    {
      if (cards.Count > index)
      {
        Card discarded = cards[index];
        cards.RemoveAt(index);
        return discarded;
      }
      else {
        return null;
      }

    }
  }
}