using System;
using System.Collections.Generic;

namespace speedwar
{
  class Deck
  {
    public List<Card> cards = new List<Card>();
    public string[] suits = {"Spades", "Hearts", "Diamonds", "Clubs"};
    public string[] ranks = {"Ace", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King"};
    public Deck()
    {
      defaultDeck();
    }
    public Deck shuffle()
    {
      int unshuffled = cards.Count;
      Card backCard;
      int randomIndex;
      Random rand = new Random();
      while (unshuffled > 0)
      {
        // Pick a random card from the unshuffled portion (subtract 1)
        // (first time through, picks number from 0 to 51):
        randomIndex = rand.Next(0,52);
        // Find the back card of the unshuffled portion
        backCard = cards[--unshuffled];
        // Switch the places of the randomly picked card and the back card
        // // Put randomly picked card at the back of the unshuffled portion
        cards[unshuffled] = cards[randomIndex];
        // // Put the back-card-that-was where the random card came from
        cards[randomIndex] = backCard;
      }
      return this;
    }
    public void printOut()
    {
      foreach (Card card in cards)
            {
                Console.WriteLine("I am the {0} of {1} and my value is {2}",
                    card.stringVal, card.suit, card.val);
            }
    }
    public void defaultDeck()
    {
      cards = new List<Card>();
      foreach (string suit in suits)
      {
        int value = 0;
        foreach (string rank in ranks)
        {
          cards.Add(new Card(rank, suit, ++value));
        }
      }
      Console.WriteLine(cards.Count);
    }
    public Card deal(){
      Card dealt = cards[0];
      cards.RemoveAt(0);
      return dealt;
    }
  }
}