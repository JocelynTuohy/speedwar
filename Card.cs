namespace speedwar
{
  public class Card
  {
    public string rank;
    public string suit;
    public int val;
    public Card(string newStringVal, string newSuit, int newVal)
    {
      rank = newStringVal;
      suit = newSuit;
      val = newVal;
    }
  }
}