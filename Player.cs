using System;
using System.Collections.Generic;

namespace speedwar
{
  public class Player
  {
    public string name;
    public Hand hand = new Hand();
    public Hand played = new Hand();
    public Hand captured = new Hand();

    public Player(string playerName="Ava")
    {
      name = playerName;
    }
    
    public void setHand(Deck deck, int numCards)
    {
      for (int i = 1; i <= numCards; ++i)
      {
        Card newCard = deck.deal();
        hand.cards.Add(newCard);
      }
    }

    public int deckSize(){
      int numCards = hand.cards.Count + played.cards.Count + captured.cards.Count;
      return numCards;
    }
    public int play(int num = 1){
      for (int i = 1; i <= num; ++i)
      {
        if (hand.cards.Count > 0)
        {
          played.cards.Add(hand.deal());
        } else
        {
          if (captured.cards.Count == 0){
            return -1;
          }else
          {
            captured.shuffle();
            foreach (Card card in captured.cards.ToArray())
            {
              hand.cards.Add(card);
            }
            captured = new Hand();
            played.cards.Add(hand.deal());
          }
        }
        
      }
      return played.cards[played.cards.Count - 1].val;
    }
    public bool checkLoser(){
      if (hand.cards.Count + played.cards.Count + captured.cards.Count == 0)
      {
        return true;
      }
      return false;
    }
  }
}