namespace speedwar
{
  class Card
  {
    public string stringVal;
    public string suit;
    public int val;
    public Card(string newStringVal, string newSuit, int newVal)
    {
      stringVal = newStringVal;
      suit = newSuit;
      val = newVal;
    }
  }
}